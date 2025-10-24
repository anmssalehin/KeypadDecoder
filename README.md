# KeypadDecoder
This repository contains solution for a C# coding challenge that I have received.


# Old Phone Keypad Decoder
A modular and extensible C# implementation that decodes strings representing keypresses on an old mobile phone keypad (like on a Nokia 3310).


# How It Works
**Tokenizer**
Converts the raw input string into Token objects (key + number of presses).
 
**KeyProcessors**
Each key type (character, backspace, etc.) is represented by a subclass of KeyProcessor that knows how to transform the input text. 

**KeyPad**
Iterates over tokens, delegates decoding to the correct processor, and builds the final string.

**PadBuilder**
Simplifies construction of a fully configured KeyPad using a fluent builder API.

**Solution.OldPhonePad()**
Public entry point — receives the input string and returns the decoded text.
 

# Project Structure
Here are the key classes of the Project

**Tokenizer**
Splits the given input string into a series of tokens. It distinguishes between two kinds of keys:
1. Characters keys: Pressed repeatedly to reach a certain character. Consecutive presses need to be grouped together. Uses special break key to break and restart a sequence.
2. Special keys: One press triggers a certain action. So, no grouping is needed.

**KeyProcessor**
Encapsulates action required for a certain token. Modifies a StringBuilder to reflect action of a token. Two kinds of KeyProcessor in implemented:
1. CharacterKeyProcessor: Processes character keys
2. BackKeyProcessor: Handles back key press
The solution can be easily extended by adding more subclasses of KeyProcessor as needed. 

**Keypad**
Combines Tokenizer and KeyProcessor to decode given input. For flexible creation, a builder class is provided.
KeyPad depends on abstractions (KeyProcessor), not concrete implementations.

**Solution**
A static class that exposes a simple interface to the solution of this challenge.
  

# Unit Tests
Located in KeypadDecoder.Tests project

  
# Running the Project
**Prerequisites: **
.NET 8 SDK

**Run from CLI: **
dotnet run --project KeypadDecoder

**Visual Studio: **
Open the solution and run


# Run Tests
dotnet test


# License
This project is provided for educational and demonstration purposes.
If submitting as part of a coding challenge, please include a link to this repository in your response.