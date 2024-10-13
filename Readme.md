# ElementalForms Project

Welcome to the **ElementalForms** project! This project provides a method to generate elemental forms of a given word, returning a `List<List<string>>` based on the input and processing logic.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)

## Introduction

The **ElementalForms** method is designed to process a word and generate its elemental representations.

## Features

- **Easy Integration**: Simple to integrate into any C# project.

## Prerequisites

- [.NET Framework](https://dotnet.microsoft.com/download) (version 4.7.2 or later recommended)
- Basic understanding of C# programming

## Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/Funminiyi-Debug/Elemental.git
    ```
## Usage

Put the word you want to generate for in the Program.cs
```   
    var myService = services.GetRequiredService<IElementalWordsService>();
    Console.WriteLine(JsonConvert.SerializeObject(myService.ElementalForms("snack")));
    Console.WriteLine(JsonConvert.SerializeObject(myService.ElementalForms("beach")));
```