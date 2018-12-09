<p align="center">
  <a href="#">
    <img src="https://github.com/IchimGabriel/iDeliver/blob/master/iDeliverDemo/iDeliver/Content/Images/header.png" alt="iDeliver logo"  height=200>
  </a>

  <p align="center">
    Sleek, intuitive, and powerful application that connects Shops and Drivers.
    <br>
    <a href="https://github.com/IchimGabriel/iDeliver/wiki"><strong>Explore iDeliver docs »</strong></a>
    <br>
    <br>
    <a href="#">Report bug</a>
    ·
    <a href="#">Request feature</a>
    ·
    <a href="#">Jobs</a>
    ·
    <a href="#">Blog</a>
  </p>
</p>

<br>

## Table of contents

- [Quick start](#quick-start)
- [What's included](#whats-included)
- [Documentation](#documentation)
- [Versioning](#versioning)
- [Creators](#creators)
- [Copyright and license](#copyright-and-license)

## Quick start

Several quick start options are available:

* Download the latest release.
* Clone the repo: git clone https://github.com/IchimGabriel/iDeliver.git

##### We did not expose the ** Web.Config**, but this page can be found in one of our commits.
After instalation open the solution in Visul Studio:
* - First run 'Enable-Migrations' in Package Manager Console
* - 'Add-Migration' and give it a name
* - 'Update-Database' to seed data

To create an Admin uncoment CreateRolesAndUsers() in Startup.cs
* - Use user name and password newly created to login
* - Navigate to Register tab and register new users with a choosen role
* - Login as new created users and test

iDeliver Demo:
https://idelivernow.azurewebsites.net/

iDeliver has an ApiController that allow other applications to use share data.
A demo can be vieew at this web address: https://idelivernow.azurewebsites.net/swagger/ui/index#/WebAPI

When you need to retrieve or manipulate information from another system, and that system provides REST APIs for that effect, you can consume a REST API in your application.
Run ** iDeliver.Client** (Console Application) to see how the API is consumed.

## What's included

Within the download you'll find the following directories and files, logically grouping common. You'll see something like this:

```
 iDeliverDemo/
    ├── iDeliver/
    │   ├── App_Start/
    │   ├── Content/
    │   ├── Controllers/
    │   ├── Models/
    │   ├── Scripts/
    │   └── Views/
    ├── iDeliver.Client/
    │   ├── Order.cs
    │   ├── AspNetUser.cs
    │   └── Program.cs
    └── iDeliver.Test/
        ├── FakeTestData.cs
        └── iDeliverTest.cs
```
## Documentation

Database - Sample Pages and iDeliver Application Tree:
https://github.com/IchimGabriel/iDeliver/wiki

## Versioning

## Creators

**Grigore Bivol**

- <https://github.com/bivolgri>

**Gabriel Ichim**

- <https://codexapi.com/gabrielichim>
- <https://github.com/IchimGabriel>

## Copyright and license
