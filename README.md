# CyberSecurittChatBot
# Chatbot Command Line Interface (CLI)

## Overview
This project implements a command line chatbot that allows users to interact through pre-defined questions and answers or leverage an AI API for dynamic responses. The goal is to create a functional and visually appealing chat interface in the terminal.

## Features
- **AI Integration**: Connects to an AI API for automated responses to user queries.
- **Pre-Defined Questions**: Users can select from a list of common questions, making it easy to receive answers without typing.
- **Dynamic Responses**: If the user's question is not recognized, the chatbot utilizes AI to provide relevant answers.
- **Enhanced Console UI**: Utilizes ASCII art and formatting techniques to improve the appearance of the chatbot interface.

## Continuous Integration Workflow
This project uses GitHub Actions for continuous integration (CI) to ensure that the code is tested and deployed automatically whenever changes are made. The CI workflow includes:
- Running automated tests
- Linting the code for style consistency
- Deploying the latest version to a specified environment

## Code and Logic Presentation
The chatbot is built using [insert programming language] and follows a modular approach:
- **Main Logic**: Handles user input and determines the response mechanism.
- **API Integration**: Communicates with the AI service to fetch responses.
- **User Interface**: Manages the display of questions and results in the console.

