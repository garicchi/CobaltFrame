param($installPath, $toolsPath, $package, $project)

$project.ProjectItems | Foreach-Object {Write-Host $_.Name}
$item = $project.ProjectItems.Item("ipagothic.fnt");

$item.Properties.Item("BuildAction").Value = [int]2
$item.Properties.Item("CopyToOutputDirectory").Value = [int]2