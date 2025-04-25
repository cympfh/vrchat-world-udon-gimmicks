# Udon scripts for VRChat worlds

`src/` 以下に Udon のスクリプトを置いています

## PickupSystem

- PickupSystem/PickupSystem.cs
    - アイテムを眼の前に呼び出す
        - VR の人は左トリガー三回押しで発動
        - PC の人は E キーで発動

| 設定項目      | 必須 | デフォルト値 | 説明                      |
|:--------------|:-----|:-------------|:--------------------------|
| Pickup Item   | Yes  | None         | アイテムのObjectを指定 |
| Item_distance | Yes  | 0.45 (くらい)| 目からの距離 |
| Hook_udon     | No   | None         | 発動したら他の UdonBehaviour を呼び出す |
| Hook_name     | No   | None         | 発動したら呼び出す関数名 (Hook_udon とセットで使う) |

- PickupSystem/ReturnSystem.cs
    - PickupSystem.cs とセットで使う前提
    - 眼の前のアイテムを初期位置に戻す
        - VR の人は右トリガー三回押しで発動
        - PC の人は F キーで発動

| 設定項目      | 必須 | デフォルト値 | 説明                      |
|:--------------|:-----|:-------------|:--------------------------|
| Pickup Item   | Yes  | None         | アイテムのObjectを指定 |
| Hook_udon     | No   | None         | 発動したら他の UdonBehaviour を呼び出す |
| Hook_name     | No   | None         | 発動したら呼び出す関数名 (Hook_udon とセットで使う) |

## FollowingSystem

- FollowingSystem.cs
    - オブジェクトを別のオブジェクトに追従させる
    - 初期状態では追従は始まらないので `Enable()` をキックしてください

| 設定項目      | 必須 | デフォルト値 | 説明                       |
|:--------------|:-----|:-------------|:---------------------------|
| Target        | Yes  | None         | 追従される側のオブジェクト |
| Followers     | Yes  | None         | 追従される側のオブジェクト（複数指定可） |
| Offset        | Yes  | None         | 追従する位置のオフセット   |
