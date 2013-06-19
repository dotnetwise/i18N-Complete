Set-PsDebug -trace 0

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

$currentLocalizationPath = $toolsPath + "\Properties\Localization\"

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
	for ($i=0; $i -lt $bytes.length; $i++) {
		$checksum += $bytes[$i].ToString('x2')
	}
    
	$stream.Close() | Out-Null
    
    return $checksum
}

function Delete-ProjectItem($itemName) {
	$sourcePath = $currentLocalizationPath + $itemName
	$destinationPath = $localizationPath + $itemName
	if (Test-Path $destinationPath) {
		$destCh = (Get-Checksum $destinationPath)
		$sourceCh = (Get-Checksum $sourcePath)
		if ($sourceCh -eq $destCh) {
			write-host $destinationPath + " -- SOURCE: "+$sourcePath 
			$itemDeleted = $false
			try {
				$item = $localizationFolderProjectItem.ProjectItems.Item($itemName)
			
				for ($i=1; $i -le 5; $i++) {
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
			} catch {
			}
			if ($itemDeleted -eq $false) {
				throw "Unable to delete $itemName after five attempts."
			}
		}
	}
}