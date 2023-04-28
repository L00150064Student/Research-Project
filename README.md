# Research Project


This repository contains the code for a project that collects sensor data from an Arduino board and sends it to a cloud storage platform called ThingSpeak. It includes code for the Arduino, Raspberry Pi, a Thingspeak channel, and a mobile application built with Dotnet 7 MAUI.
ThingSpeak channel: https://thingspeak.com/channels/1954302
Getting Started

To get started, clone the repository to your local machine. You'll need the following IDEs and tools installed:

    Raspberry Pi OS
        - inc python serial
        - inc Arduino IDE on Raspberry Pi OS
    Viaual Studio 2022
        - .NET 7 specifications
        - MAUI SDK (with following nuGet packages)
            - RestSharp
            - CommunityToolkit.MVVM
            - Newtonsoft JSON

# Folder Structure

The repository is organized into the following folders:

    arduino: contains the code for the Arduino board that collects sensor data and sends it to the Raspberry Pi via serial cable.
    raspberry-pi: contains the code for the Raspberry Pi that receives data from the Arduino and sends it to ThingSpeak using Python.
    thingspeak: contains URLs and channel fields required for ThingSpeak cloud storage.
    mobile-app: contains the code for a Dotnet 7 MAUI mobile application that retrieves data from ThingSpeak via REST API.

