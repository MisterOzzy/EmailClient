echo 'Running update script'

$contentAssembly = Get-Content EmailClient\SharedAssemblyInfo.cs
"'"+$contentAssembly+"'" -match '\[assembly: AssemblyVersion\(\"(?<major>\d+).(?<minor>\d+).(?<patch>\d+).(\*)\"\)\]'
$matchVar = $matches
$versionNew = "{0}.{1}.{2}" -f $matchVar["major"],$matchVar["minor"],$matchVar["patch"]

$contentInstaller = Get-Content Installer\setup_CI.iss
"'"+$contentInstaller+"'" -match '\#define MyAppVersion \"(?<major>\d+).(?<minor>\d+).(?<patch>\d+)\"'
$matchVar1 = $matches
$versionOld = "{0}.{1}.{2}" -f $matchVar1["major"],$matchVar1["minor"],$matchVar1["patch"]

(Get-Content Installer\setup_CI.iss) | 
Foreach-Object {$_ -replace $versionOld,$versionNew} | 
Out-File -Encoding ASCII Installer\setup_CI.iss

echo 'Update completed'