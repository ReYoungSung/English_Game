**한국어** | [English](README.en.md)

# English Game

한국어 뜻을 보고 영어 단어 버튼을 조합하는 Unity 기반 영어 문장 학습 게임입니다. 챕터·유닛별 학습 데이터를 CSV에서 읽으며 연습 모드와 테스트 모드를 구분합니다.

## 주요 구성

- 챕터·유닛·레벨 선택과 문제 표시
- 영어 정답 문장 및 오답 선택지를 이용한 단어 버튼 구성
- 연습·테스트용 게임 씬과 결과 씬
- 인앱 결제 및 Google Play Games 연동 코드

서비스 연동 코드가 포함되어 있다는 사실과 실제 스토어에서의 동작·배포 상태는 별개입니다.

## 개발 환경

| 항목 | 값 |
| --- | --- |
| Unity Editor | **2022.3.42f1** |
| Unity 프로젝트 폴더 | 저장소 루트 |
| 기본 브랜치 | `main` |
| 시작 씬 | `Assets/Scenes/1 Menu/IntroScene.unity` |

설정 근거: [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt) · [패키지 목록](Packages/manifest.json) · [빌드 씬 목록](ProjectSettings/EditorBuildSettings.asset)

## 시작하기

```bash
git clone --branch main https://github.com/ReYoungSung/English_Game.git
```

1. Unity Hub에 Unity 2022.3.42f1을 설치하고 복제한 저장소 루트를 프로젝트로 추가합니다.
2. 최초 임포트와 패키지 복원을 완료합니다.
3. `Assets/Scenes/1 Menu/IntroScene.unity`를 열고 Play를 실행합니다.
4. 메뉴에서 챕터·유닛을 선택하고 연습 또는 테스트 흐름을 확인합니다.

`GameManager`는 `SceneOption.Instance`의 챕터·유닛·레벨 값을 사용하므로 개별 게임 씬만 바로 실행하면 초기화 상태가 부족할 수 있습니다. Android 빌드는 해당 Editor의 Android Build Support 모듈과 서비스별 설정이 필요합니다.

## 학습 데이터

실제로 로드하는 데이터는 [Assets/Resources/](Assets/Resources/)에 있습니다.

| 파일 규칙 | 용도 |
| --- | --- |
| `Chapter{번호}_KSP.csv` | 한국어 뜻 |
| `Chapter{번호}_ESP.csv` | 영어 정답 문장 |
| `Chapter{번호}_FSP.csv` | 오답 선택지 |

`CSVReader`는 `Resources.Load`로 파일을 읽습니다. 저장소 루트의 `Chapter1_FSP.csv`만 수정해도 게임에 반영되는 것은 아니므로 `Assets/Resources/`의 실제 데이터를 확인합니다. 영어 문장은 `/`로 단어를 구분하며, 해당 구분자가 없으면 공백으로 나눕니다. 오답 선택지는 `&`로 구분합니다.

## 주요 코드

| 경로 | 역할 |
| --- | --- |
| [Assets/script/SystemBasicScripts/GameManager.cs](Assets/script/SystemBasicScripts/GameManager.cs) | 문제·정답·오답 데이터 구성 |
| [Assets/script/CSVSystem/CSVReader.cs](Assets/script/CSVSystem/CSVReader.cs) | CSV 로드 |
| [Assets/script/SceneOption.cs](Assets/script/SceneOption.cs) | 챕터·유닛·레벨 상태 |
| [Assets/script/IAPSystem/](Assets/script/IAPSystem/) | 인앱 결제 및 잠금 해제 |
| [Assets/Scenes/](Assets/Scenes/) | 메뉴·학습·테스트·결과 씬 |

## 사용법

### 학습 진행

1. 시작 화면을 거쳐 메뉴에서 학습할 챕터와 유닛을 선택합니다.
2. 처음에는 연습 모드로 시작합니다. 한국어 뜻과 영어 문장을 확인하고, 영어 단어가 적힌 풍선 버튼을 **문장 순서대로** 누릅니다.
3. 올바른 순서로 문장을 완성하면 성공 효과가 표시됩니다. 연습 모드는 같은 문제를 3회 성공한 뒤 다음 문제로 넘어갑니다.
4. 테스트 모드에서는 문장을 한 번 완성하면 다음 문제로 이동합니다. 잘못된 단어나 순서를 선택하면 오류 횟수가 증가하고 문제를 다시 시작합니다.
5. 테스트 중 오류 횟수가 3회 이상이면 게임 오버 화면으로 이동합니다. 마지막 문제를 완료하면 모드에 맞는 결과 화면이 열리고 진행 상태를 저장합니다.

| 조작 | 동작 |
| --- | --- |
| 단어 버튼 터치 또는 클릭 | 선택한 단어를 현재 문장 뒤에 추가하고 순서 판정 |
| 올바른 문장 완성 | 연습 반복 또는 다음 문제로 이동 |
| 잘못된 단어·순서 선택 | 실패 효과 후 문제 재시작; 테스트는 오류 횟수 누적 |

현재 코드의 유닛당 마지막 문제 번호는 연습 15, 테스트 9입니다. 잠긴 챕터·유닛은 저장된 진행 상태와 라이선스 확인에 영향을 받습니다. 결제·계정 기능은 실제 서비스 설정이 갖춰진 환경에서 별도로 확인합니다.

입력 후 반응이 없으면 게임 화면을 활성화하고 UI가 다른 창에 가려져 있지 않은지 확인합니다. 문제가 비어 있으면 선택한 챕터의 CSV와 유닛 열 번호를 확인합니다.

동작 기준: [ButtonGame.cs](Assets/script/SystemBasicScripts/ButtonGame.cs) · [SceneOption.cs](Assets/script/SceneOption.cs)
