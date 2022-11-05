dotnet build --configuration Release

# update manifest
$xml = [Xml] (Get-Content ".\ModManager\ModManager.csproj")
$manifest = Get-Content ".\manifest.json" | ConvertFrom-Json

$modversion = $xml.Project.PropertyGroup.Version
$desc = $xml.Project.PropertyGroup.Description

Write-Output "Mod Version: $modversion"
Write-Output "Description: $desc"

$manifest.description = $desc
$manifest.version_number = $modversion

$manifest | ConvertTo-Json | Out-File ".\manifest.json"

New-Item -ItemType Directory ".\Thunderstore\ModManager\" -Force
Copy-Item -Path ".\ModManager\bin\Release\net6.0\win-x64\*.dll", ".\ModManager\*.png" -Destination ".\Thunderstore\ModManager"
Copy-Item -Path ".\icon.png", ".\manifest.json", ".\README.md" -Destination ".\Thunderstore"
Compress-Archive -Path ".\Thunderstore\*" -CompressionLevel "Optimal" -DestinationPath ".\ModManager-$modversion.zip" -Force
Remove-Item -Path ".\Thunderstore\" -Recurse