# SmtpLW - Locaweb SMTP API client

The [Locaweb dedicated SMTP service](https://www.locaweb.com.br/smtp-locaweb/) API client.

[![GitHub license](https://img.shields.io/github/license/guibranco/SmtpLw)](https://github.com/guibranco/SmtpLw)
[![Time tracker](https://wakatime.com/badge/github/guibranco/SmtpLw.svg)](https://wakatime.com/badge/github/guibranco/SmtpLw)

![SmtpLw logo](logo.jpg)

## CI/CD

| Build status | Last commit | Tests | Coverage | Code Smells | LoC |
|--------------|-------------|-------|----------|-------------|-----|
| [![Build status](https://ci.appveyor.com/api/projects/status/b7xakaaq8ldqp34r/branch/main?svg=true)](https://ci.appveyor.com/project/guibranco/smtplw) | [![GitHub last commit](https://img.shields.io/github/last-commit/guibranco/smtplw)](https://github.com/guibranco/smtplw) | [![AppVeyor tests (branch)](https://img.shields.io/appveyor/tests/guibranco/smtplw/main?compact_message)](https://ci.appveyor.com/project/guibranco/smtplw) | [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=guibranco_SmtpLw\&metric=coverage)](https://sonarcloud.io/dashboard?id=guibranco_SmtpLw) | [![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=guibranco_SmtpLw\&metric=code_smells)](https://sonarcloud.io/dashboard?id=guibranco_SmtpLw) | [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=guibranco_SmtpLw\&metric=ncloc)](https://sonarcloud.io/dashboard?id=guibranco_SmtpLw) |

## Code Quality

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/5ee8a21e3f7a4ccdb614e8856c48d5f3)](https://www.codacy.com/gh/guibranco/SmtpLw/dashboard?utm_source=github.com\&utm_medium=referral\&utm_content=guibranco/SmtpLw\&utm_campaign=Badge_Grade)
[![Codacy Badge](https://app.codacy.com/project/badge/Coverage/5ee8a21e3f7a4ccdb614e8856c48d5f3)](https://www.codacy.com/gh/guibranco/SmtpLw/dashboard?utm_source=github.com\&utm_medium=referral\&utm_content=guibranco/SmtpLw\&utm_campaign=Badge_Grade)
[![Codecov](https://codecov.io/gh/guibranco/smtplw/branch/main/graph/badge.svg)](https://codecov.io/gh/guibranco/smtplw)

[![Maintainability](https://api.codeclimate.com/v1/badges/46804babd27cdd40d219/maintainability)](https://codeclimate.com/github/guibranco/SmtpLw/maintainability)
[![Test Coverage](https://api.codeclimate.com/v1/badges/46804babd27cdd40d219/test_coverage)](https://codeclimate.com/github/guibranco/SmtpLw/test_coverage)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=guibranco_SmtpLw\&metric=alert_status)](https://sonarcloud.io/dashboard?id=guibranco_SmtpLw)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=guibranco_SmtpLw\&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=guibranco_SmtpLw)

[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=guibranco_SmtpLw\&metric=sqale_index)](https://sonarcloud.io/dashboard?id=guibranco_SmtpLw)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=guibranco_SmtpLw\&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=guibranco_SmtpLw)

[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=guibranco_SmtpLw\&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=guibranco_SmtpLw)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=guibranco_SmtpLw\&metric=security_rating)](https://sonarcloud.io/dashboard?id=guibranco_SmtpLw)

[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=guibranco_SmtpLw\&metric=bugs)](https://sonarcloud.io/dashboard?id=guibranco_SmtpLw)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=guibranco_SmtpLw\&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=guibranco_SmtpLw)

***

## Installation

### Github Releases

[![GitHub last release](https://img.shields.io/github/release-date/guibranco/SmtpLw.svg?style=flat)](https://github.com/guibranco/SmtpLw) [![Github All Releases](https://img.shields.io/github/downloads/guibranco/SmtpLw/total.svg?style=flat)](https://github.com/guibranco/SmtpLw)

Download the latest zip file from the [Release](https://github.com/GuiBranco/SmtpLw/releases) page.

### Nuget package manager

| Package | Version | Downloads |
|------------------|:-------:|:-------:|
| **SmtpLw** | [![SmtpLw NuGet Version](https://img.shields.io/nuget/v/SmtpLw.svg?style=flat)](https://www.nuget.org/packages/SmtpLw/) | [![SmtpLw NuGet Downloads](https://img.shields.io/nuget/dt/SmtpLw.svg?style=flat)](https://www.nuget.org/packages/SmtpLw/) |

***

## Features

Implements all features of Locaweb SMTP API available at [SMTP Localweb API docs](http://developer.locaweb.com.br/documentacoes/smtp/api-como-utilizar/)

*   Send message
*   Get message status

***

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

***
