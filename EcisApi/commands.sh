#!/bin/bash
dotnet-ef --project ./ database update
dotnet EcisApi.dll --server.urls "http://*:5000"