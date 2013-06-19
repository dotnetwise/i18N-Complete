[string[]] $filesArray = "Excludes.txt", "FileExtensions.txt", "Languages.txt", "Localize.bat", "messages.de.po", "messages.po", "messages.ro.po", "newmessages.pot"

try {
	$propertiesFolderProjectItem = $project.ProjectItems.Item("Properties")
    $propertiesPath = $propertiesFolderProjectItem.FileNames(1)
} catch {
	Write-Host "Properties folder not found." -ForegroundColor Magenta
	exit
}

try {
	$localizationFolderProjectItem = $propertiesFolderProjectItem.ProjectItems.Item("Localization")
    $localizationPath = $localizationFolderProjectItem.FileNames(1)
} catch {
	Write-Host "Localization folder not found." -ForegroundColor Magenta
	exit
}

Write-Host $localizationFolderProjectItem.ProjectItems.Item("Localize.bat") -ForegroundColor Red
Write-Host $localizationPath -ForegroundColor Blue
Write-Host $propertiesPath -ForegroundColor Blue
$currentLocalizationPath = $toolsPath + "\Properties\Localization\"
Write-Host $currentLocalizationPath -ForegroundColor Blue

function Get-Checksum($file) {
    $cryptoProvider = New-Object "System.Security.Cryptography.MD5CryptoServiceProvider"
	
    $fileInfo = Get-Item $file
	trap { ;
	continue } $stream = $fileInfo.OpenRead()
    if ($? -eq $false) {
		# Couldn't open file for reading
        return $null
	}
    
    $bytes = $cryptoProvider.ComputeHash($stream)
    $checksum = ''
	foreach ($byte in $bytes) {
		$checksum += $byte.ToString('x2')
	}
    
	$stream.Close() | Out-Null
    
    return $checksum
}

function Delete-ProjectItem($itemName) {
	$sourcePath = $currentLocalizationPath + $itemName
	$destinationPath = $localizationPath + $itemName
	if (Test-Path $destinationPath) {
		if ((Get-Checksum $destinationPath) -eq (Get-Checksum $sourcePath)) {
			$itemDeleted = $false
			$item = $localizationFolderProjectItem.ProjectItems.Item($itemName)
			for ($1=1; $i -le 5; $i++) {
				try {
					$item.Delete()
					$itemDeleted = $true
					break
				}
				catch {
					# Try again in 200ms
					[System.Threading.Thread]::Sleep(200)
				}
			}
			if ($itemDeleted -eq $false) {
				throw "Unable to delete project item after five attempts."
			}
		}
	}
}