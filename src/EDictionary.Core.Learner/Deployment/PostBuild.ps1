param(
   [String] $SolutionDir,
   [String] $TargetDir)

$ErrorActionPreference = "Stop"

$Prompt = "Core>"

$SourceDir = "${SolutionDir}EDictionary\Data"

$TargetSqlitePath = "${TargetDir}words.sqlite"
$TargetSqliteDir = "$TargetDir"

# $SourceAudioPath = "${SolutionDir}..\tool\data\audio"
$TargetAudioPath = "${TargetDir}audio"

Write-Host "${Prompt} Running PostBuild script..."

Expand-Archive "$SourceDir\words.zip" -DestinationPath "$SourceDir" -Force

if (!(Test-Path -Path "$TargetSqlitePath"))
{
	Write-Host "${Prompt} sqlite file not found. Copying new file to target dir..."
	Move-Item -Path "$SourceDir\words.sqlite" -Destination "$TargetSqliteDir"
}
else
{
	# copy sqlite file if filesize not equal
	$sourceSqliteFile = Get-ChildItem "$SourceDir\words.sqlite"
	$targetSqliteFile = Get-ChildItem "$TargetSqlitePath"

	if (Compare-Object $sourceSqliteFile $targetSqliteFile -Property Length)
	{
		Write-Host "${Prompt} Detect sqlite file size changed ($($sourceSqliteFile.Length) vs $($targetSqliteFile.Length)). Copying new file to target dir..."
		Move-Item -Path "$SourceDir\words.sqlite" -Destination "$TargetSqliteDir"
	}
}

if (Test-Path "$SourceDir\words.sqlite") 
{
  Remove-Item "$SourceDir\words.sqlite"
}

if (!(Test-Path -Path "$TargetAudioPath"))
{
	Write-Host "${Prompt} audio dir not found. Copying audio files to target dir..."

	& "${SourceDir}\7za.exe" x "${SourceDir}\audio.zip.001"
}

# vim: ft=conf
