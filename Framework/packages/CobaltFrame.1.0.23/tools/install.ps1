param($installPath, $toolsPath, $package, $project)
function Recurse($dir){
    foreach($i in $dir)
    {
        Recurse($i.ProjectItems)
        if($i.Name -eq "ipagothic.fnt")
        {
            $i.Properties.Item("BuildAction").Value = [int]2
            $i.Properties.Item("CopyToOutputDirectory").Value = [int]1
        }
    }
}

Recurse($project.ProjectItems);
