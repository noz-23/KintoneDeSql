# KintoneDeSql

## 1.概要

Kintone(Cybozu) API で取得できるJSONデータを変換しデータベースへ登録できるアプリです。

基本はKintoneアプリのテーブル(サブテーブル)もデータベースにしてSQLで操作する目的となりますが、

Kintone API で取得できるデータは基本全てデータベース化できます。


## 2.注意点

Kintone API の取得(GET)のみの対応で、追加更新(POST/PUT)はできません。

再取得でデータベースは更新しますが、削除はできません。

## 3.今後

操作説明等がアプリ内にないため、追加していきたいと思います。

## 4.ライセンス

MIT license https://mit-license.org/

## 5.利用

Dapper : Apache-2.0

 https://github.com/DapperLib/Dapper

SQLite : Public domain

　https://www.sqlite.org/index.html

## 6.処理参考

kintoneDotNET : Apache-2.0 license

　https://github.com/icoxfog417/kintoneDotNET/wiki/How-to-use-kintoneDotNET

## 7.履歴

 0.1.a  : 2025/06/12 とりあえず公開　開始

 0.1.0  : 2025/06/22 一通り完成版

 ## 8.使い方


 ### Setting

「Setting>Kintone」の「Kintone Domain」のみ必須です

![Image](https://github.com/user-attachments/assets/8b3a118a-64ee-463b-909e-091795ba6ebe)

![Image](https://github.com/user-attachments/assets/e40979c0-d92d-476a-ab06-8b9c662bb704)

同設定画面で「Account(アカウント)」情報を入れるか、起動画面での青の項目「Table Name(任意)」「App Id(アプリ番号)」「Api Key(Web値)」を入れて利用します

アカウント情報がある場合は、「Get」ボタン押下でアプリ情報を取得できます(権限がある場合)

基本、情報取得は「Get」ボタンを押下して下さい

アプリのみ起動画面の「Insert」でデータ取得、「Select」でデータ表示となります

![Image](https://github.com/user-attachments/assets/59700123-5918-4397-b421-7a8fe86d4613)

![Image](https://github.com/user-attachments/assets/ecacd0f6-0648-41ae-a1d4-5d03cdd277a7)


「Execute」でSQLを実行できます

![Image](https://github.com/user-attachments/assets/eb0fe307-75cf-448f-9d47-85d17564afbb)


「Tables」 で各データベース(ファイル)のテーブルとカラム名を確認し、選択したテーブルのSQL文を作成できます

![Image](https://github.com/user-attachments/assets/bfe058d6-8e73-4ece-a887-f67a8ef8c1c5)

「Join」で複数のテーブルを結合するSQL文を作成できます

![Image](https://github.com/user-attachments/assets/07de6514-16e2-440c-8616-d12723c25903)
