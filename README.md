# QRCodeReader
Multiplatform QR Code Reader Console App

Syntax
./QRCodeReader <image file path>

Build
dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained false
dotnet publish -r linux-x64 -p:PublishSingleFile=true --self-contained false
