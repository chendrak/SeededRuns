dotnet build --configuration Release

$modname = "SeededRuns"

# update manifest
$xml = [Xml] (Get-Content ".\$modname\$modname.csproj")
$manifest = Get-Content ".\manifest.json" | ConvertFrom-Json

$modversion = $xml.Project.PropertyGroup.Version
$desc = $xml.Project.PropertyGroup.Description

Write-Output "Mod Version: $modversion"
Write-Output "Description: $desc"

$manifest.name = $modname
$manifest.description = $desc
$manifest.version_number = $modversion

$manifest | ConvertTo-Json | Out-File ".\manifest.json"

New-Item -ItemType Directory ".\Thunderstore\$modname\" -Force
Copy-Item -Path ".\$modname\bin\Release\net6.0\win-x64\*.dll", ".\$modname\*.png" -Destination ".\Thunderstore\$modname"
Copy-Item -Path ".\icon.png", ".\manifest.json", ".\README.md" -Destination ".\Thunderstore"
Compress-Archive -Path ".\Thunderstore\*" -CompressionLevel "Optimal" -DestinationPath ".\$modname-$modversion.zip" -Force
Remove-Item -Path ".\Thunderstore\" -Recurse