[한국어](README.md) | **English**

# English Game

A Unity-based English sentence-learning game in which players read a Korean meaning and combine English word buttons. Chapter and unit data are loaded from CSV files, with separate practice and test modes.

## Features

- Chapter, unit, and level selection with question display
- Word buttons built from the correct English sentence and distractors
- Separate practice, test, and result scenes
- Integration code for in-app purchases and Google Play Games

The presence of service integration code does not establish that the integrations currently work or that the app is available in a store.

## Development environment

| Item | Value |
| --- | --- |
| Unity Editor | **2022.3.42f1** |
| Unity project folder | Repository root |
| Default branch | `main` |
| Entry scene | `Assets/Scenes/1 Menu/IntroScene.unity` |

Configuration references: [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt) · [Packages](Packages/manifest.json) · [Build scenes](ProjectSettings/EditorBuildSettings.asset)

## Getting started

```bash
git clone --branch main https://github.com/ReYoungSung/English_Game.git
```

1. Install Unity 2022.3.42f1 through Unity Hub and add the cloned repository root as a project.
2. Complete the initial import and package restoration.
3. Open `Assets/Scenes/1 Menu/IntroScene.unity` and enter Play mode.
4. Select a chapter and unit from the menu, then try the practice or test flow.

`GameManager` uses the chapter, unit, and level values in `SceneOption.Instance`. Starting an individual gameplay scene directly may therefore leave required state uninitialized. Android builds require the Editor's Android Build Support module and configuration for the relevant services.

## Learning data

The data actually loaded by the game is in [Assets/Resources/](Assets/Resources/).

| File pattern | Purpose |
| --- | --- |
| `Chapter{number}_KSP.csv` | Korean meanings |
| `Chapter{number}_ESP.csv` | Correct English sentences |
| `Chapter{number}_FSP.csv` | Distractors |

`CSVReader` reads files using `Resources.Load`. Editing only `Chapter1_FSP.csv` at the repository root does not necessarily change the game; check the data in `Assets/Resources/`. English sentences use `/` as a word separator, falling back to spaces when it is absent. Distractors are separated by `&`.

## Main code

| Path | Responsibility |
| --- | --- |
| [Assets/script/SystemBasicScripts/GameManager.cs](Assets/script/SystemBasicScripts/GameManager.cs) | Question, answer, and distractor data |
| [Assets/script/CSVSystem/CSVReader.cs](Assets/script/CSVSystem/CSVReader.cs) | CSV loading |
| [Assets/script/SceneOption.cs](Assets/script/SceneOption.cs) | Chapter, unit, and level state |
| [Assets/script/IAPSystem/](Assets/script/IAPSystem/) | In-app purchases and unlocking |
| [Assets/Scenes/](Assets/Scenes/) | Menu, practice, test, and result scenes |

## Usage

### Learning flow

1. Proceed through the initial screen and select a chapter and unit.
2. Start with practice mode. Read the Korean meaning and English sentence, then select the balloon-shaped word buttons **in sentence order**.
3. Completing the sentence correctly displays a success effect. Practice mode requires three successful completions of the same question before advancing.
4. Test mode advances after one successful completion. Selecting an incorrect word or order increments the error count and restarts the question.
5. Three or more errors during a test lead to the game-over screen. Completing the final question opens the result screen for the current mode and saves progress.

| Control | Action |
| --- | --- |
| Tap or click a word button | Append the word to the current sentence and check its order |
| Complete the correct sentence | Repeat the practice question or advance |
| Select an incorrect word/order | Show a failure effect and restart; test mode accumulates errors |

In the current code, the final question number per unit is 15 for practice and 9 for tests. Locked chapters and units depend on saved progress and license checks. Verify purchase and account features separately in an environment with the actual service configuration.

If input does not respond, focus the game view and check that another window is not covering the UI. If a question is empty, check the selected chapter's CSV and unit column index.

Behavior references: [ButtonGame.cs](Assets/script/SystemBasicScripts/ButtonGame.cs) · [SceneOption.cs](Assets/script/SceneOption.cs)
