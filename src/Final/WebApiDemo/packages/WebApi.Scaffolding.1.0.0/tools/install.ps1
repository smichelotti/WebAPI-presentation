param($rootPath, $toolsPath, $package, $project)

# Bail out if scaffolding is disabled (probably because you're running an incompatible version of NuGet)
if (-not (Get-Command Invoke-Scaffolder)) { return }

if ($project) { $projectName = $project.Name }
Get-ProjectItem "InstallationDummyFile.txt" -Project $projectName | %{ $_.Delete() }

# Could use "Set-DefaultScaffolder" here if desired