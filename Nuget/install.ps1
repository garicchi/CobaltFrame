param($installPath, $toolsPath, $package, $project)

$item = $project.ProjectItems | where-object {$_.Extension -eq ".fnt"}

$item.Properties.Item("BuildAction").Value = [int]2