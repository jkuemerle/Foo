Import-Module "C:\Program Files (x86)\Microsoft SDKs\Windows Azure\PowerShell\Azure\Azure.psd1"

$label = "PowerShellDeployment" 
$service = "JoeSoftPSDeploy"
$sub = get-content "c:\temp\AzureSubID.txt"
$thumb = get-content "c:\temp\JoeSoftDeployThumbprint.txt"
$storageKey = get-content "c:\temp\AzureStorageKey.txt"
$cert = Get-Item cert:\LocalMachine\My\"$thumb"
$subName = get-content "c:\temp\AzureSubName.txt"
$packageFile = "C:\src\AzureCloudServices\CloudService\bin\Release\app.publish\CloudService.cspkg"
$configFile = "C:\src\AzureCloudServices\CloudService\bin\Release\app.publish\ServiceConfiguration.Cloud.cscfg"
$storageName = "joesoftpsdeploy"
$location = "East US"

$guid = [guid]::NewGuid().ToString()
$packageBlobName = "CloudService-" + $guid + ".cspkg"

$packageURL = "http://joesoftpsdeploy.blob.core.windows.net/deploy/$packageBlobName"

Set-AzureSubscription -SubscriptionName $subName -Certificate $cert -SubscriptionID $sub -CurrentStorageAccount $storageName
Select-AzureSubscription -SubscriptionName $subName

$context = New-AzureStorageContext -StorageAccountName $storageName -StorageAccountKey $storageKey -Protocol HTTP

#new-azurestorageaccount -StorageAccountName $storageName -label $service -Location "East US"

#new-azurestoragecontainer -Name deploy -context $context

set-AzureStorageBlobContent -Container deploy -File $packageFile -Context $context -Blob $packageBlobName

new-AzureService -ServiceName $service -Label $label -Location $location 

New-AzureDeployment -serviceName $service -slot staging -package $packageURL -Configuration $configFile 

Move-AzureDeployment -ServiceName $service
