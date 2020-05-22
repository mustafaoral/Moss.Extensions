$versionSuffix = [string]::Format("{0:yyyyMMdd}.{1}", [DateTime]::Now, [Convert]::ToInt32(([DateTime]::Now - [DateTime]::Today).TotalSeconds))

dotnet clean --configuration Release
dotnet pack --configuration Release --verbosity Normal Moss.Extensions\Moss.Extensions.csproj --version-suffix $versionSuffix

$filename = dir .\Moss.Extensions\bin\Release\*$versionSuffix.nupkg | select Name
nuget add ".\Moss.Extensions\bin\Release\$($filename.Name)" -s d:\dev\local-nuget-feed
