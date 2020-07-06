# SMTPLW - SMTP Locaweb API client

The Locaweb dedicated SMTP service API client.

![SMTPLW - SMTP Locaweb](https://raw.githubusercontent.com/guibranco/smtplw/master/logo.jpg)

## CI/CD

[![Build status](https://ci.appveyor.com/api/projects/status/b7xakaaq8ldqp34r?svg=true)](https://ci.appveyor.com/project/guibranco/smtplw)
[![GitHub last commit](https://img.shields.io/github/last-commit/guibranco/smtplw)](https://github.com/guibranco/smtplw)
[![GitHub last release](https://img.shields.io/github/release-date/guibranco/smtplw.svg?style=flat)](https://github.com/guibranco/smtplw)
[![GitHub license](https://img.shields.io/github/license/guibranco/smtplw)](https://github.com/guibranco/smtplw)
[![time tracker](https://wakatime.com/badge/github/guibranco/SmtpLw.svg)](https://wakatime.com/badge/github/guibranco/SmtpLw)

## Code Quality

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/fcc8c6f773fd4aa3a286cd47e5fd8d3f)](https://www.codacy.com/manual/guilherme_9/SmtpLw)
[![Codacy Badge](https://api.codacy.com/project/badge/Coverage/fcc8c6f773fd4aa3a286cd47e5fd8d3f)](https://www.codacy.com/manual/guilherme_9/SmtpLw)
[![Codecov](https://codecov.io/gh/guibranco/smtplw/branch/master/graph/badge.svg)](https://codecov.io/gh/guibranco/smtplw)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=guibranco_smtplw&metric=alert_status)](https://sonarcloud.io/dashboard?id=guibranco_smtplw)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=guibranco_smtplw&metric=coverage)](https://sonarcloud.io/dashboard?id=guibranco_smtplw)

[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=guibranco_smtplw&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=guibranco_smtplw)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=guibranco_smtplw&metric=ncloc)](https://sonarcloud.io/dashboard?id=guibranco_smtplw)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=guibranco_smtplw&metric=sqale_index)](https://sonarcloud.io/dashboard?id=guibranco_smtplw)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=guibranco_smtplw&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=guibranco_smtplw)

[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=guibranco_smtplw&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=guibranco_smtplw)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=guibranco_smtplw&metric=security_rating)](https://sonarcloud.io/dashboard?id=guibranco_smtplw)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=guibranco_smtplw&metric=code_smells)](https://sonarcloud.io/dashboard?id=guibranco_smtplw)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=guibranco_smtplw&metric=bugs)](https://sonarcloud.io/dashboard?id=guibranco_smtplw)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=guibranco_smtplw&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=guibranco_smtplw)

---

## Installation

[![PacakgeName NuGet Version](https://img.shields.io/nuget/v/SmtpLw.svg?style=flat)](https://www.nuget.org/packages/SmtpLw/)
[![PacakgeName NuGet Downloads](https://img.shields.io/nuget/dt/SmtpLw.svg?style=flat)](https://www.nuget.org/packages/SmtpLw/)
[![Github All Releases](https://img.shields.io/github/downloads/guibranco/SmtpLw/total.svg?style=flat)](https://github.com/guibranco/smtplw)

Download the latest zip file from the [Release pages](https://github.com/guibranco/SmtpLw/releases) or simple install from [NuGet](https://www.nuget.org/packages/SmtpLw) package manager.

NuGet URL: [https://www.nuget.org/packages/SmtpLw](https://www.nuget.org/packages/SmtpLw)

NuGet installation via *Package Manager Console*:

```ps

Install-Package SmtpLw

```

---

## Features

Implements all features of Locaweb SMTP API available at [SMTP Localweb API docs](http://developer.locaweb.com.br/documentacoes/smtp/api-como-utilizar/)

- Send message
- Get message status

---

## Usage

```cs

var client = new SmtpLwClient("my-auth-token");

var message = new MessageModel {
  Subject = "Some mail subject",
  Body = "Message body content!<br /> <b>HTML content</b> or plain text.",
  To = "destination@domain.com",
  From = "configured-from@mydomain.com",
  Headers = new Dictionary<string, string> { { "content-type","text/html"} }
};

var messageId = await client.SendMessageAsync(message, CancellationToken.None).ConfigureAwait(false);

Console.WriteLine("Message Id: {0}", messageId);

```

---
