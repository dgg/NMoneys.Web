[CmdletBinding()]
Param(
	[Parameter(Mandatory=$True)]
	[string]$token,
    [Parameter(Mandatory=$True)]
	[string]$project_id
)

& iron_worker upload purge_pending --token $token --project-id $project_id
if ($lastexitcode -ne 0) {
    throw ("Could not upload worker/s. Did you remember to install iron_worker_ng gem and provide the right credentials?")
}
