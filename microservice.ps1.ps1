#./microservice.ps1 install
#./microservice.ps1 run
#./microservice.ps1 infra up
#./microservice.ps1 infra down
#./microservice.ps1 configure-cs
#./microservice.ps1 migrate

$action = $args[0]
$subaction = $args[1]

if (!$action) {
    Write-Host("`nPlease mention what action to perform`n")
    exit;
}

$action = $action.ToLower();

if ($action -eq "install") {
    Invoke-Expression "./etc/setup.ps1"
}
elseif ($action -eq "infra") {
    if ($subaction -eq "up") {
        Invoke-Expression "./etc/docker/up.ps1"
    }
    elseif ($subaction -eq "down") {

        Invoke-Expression "./etc/docker/down.ps1"
    }
}
elseif ($action -eq "configure-cs") {
    Invoke-Expression "./etc/configure-cs.ps1"
}
elseif ($action -eq "run") {
    Invoke-Expression "./etc/run.ps1"
}
elseif ($action -eq "migrate") {
    Invoke-Expression "./etc/migrate.ps1"
}
