$ErrorActionPreference = "Stop"

$listener = New-Object System.Net.HttpListener
$listener.Prefixes.Add("http://127.0.0.1:8090/billing/")
$listener.Start()

Write-Host "Billing mock listening on http://127.0.0.1:8090/billing/"

try {
    while ($listener.IsListening) {
        $context = $listener.GetContext()
        $path = $context.Request.Url.AbsolutePath.ToLowerInvariant()

        if ($path.EndsWith("/billing/billing_serverstate.asp")) {
            $body = "1"
        }
        else {
            $body = "OK"
        }

        $bytes = [System.Text.Encoding]::ASCII.GetBytes($body)
        $context.Response.StatusCode = 200
        $context.Response.ContentType = "text/plain"
        $context.Response.ContentLength64 = $bytes.Length
        $context.Response.OutputStream.Write($bytes, 0, $bytes.Length)
        $context.Response.OutputStream.Close()
    }
}
finally {
    $listener.Stop()
    $listener.Close()
}
