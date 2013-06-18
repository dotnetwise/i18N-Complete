#First some common params, delivered by the nuget package installer
param($installPath, $toolsPath, $package, $project)
Set-PsDebug -trace 2
$buildProject = Get-MSBuildProject

$target = $buildProject.Xml.Targets | 
                   where { $_.Name -eq "Localization" }
if($target -ne $null)
{
    $buildProject.Xml.RemoveChild($target)
	throw "Removed Localization!"
}

# Save the buildproject
$buildProject.Save()
# Save the project from the params
$project.Save()