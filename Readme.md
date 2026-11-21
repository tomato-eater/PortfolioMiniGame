# PortfolioMiniGame

Unity製の2Dミニゲーム（ポートフォリオ作品）です。

## 動作環境

- Unity 6000.3.8f1
- Input System / 2D Tilemap

## 構成

| パス | 内容 |
| --- | --- |
| `Assets/Scenes` | シーン |
| `Assets/Scripts` | ゲームロジック（Player, GameManager, StageManager, ScoreManager など） |
| `Assets/Scripts/UI` | UI関連スクリプト |
| `Assets/Prefabs` | プレハブ |
| `Assets/Date` | ステージ一覧データ（StageList） |

## 開始方法

1. リポジトリをクローンします。
2. Unity Hub で Unity 6000.3.8f1 を使ってプロジェクトを開きます。
3. `Assets/Scenes` 内のシーンを開いて再生します。

## ブランチ運用

- `main`: 安定版
- `develop`: 開発用
- 作業は `develop` からブランチを切り、PRで取り込みます。