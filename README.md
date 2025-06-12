# KintoneDeSql

## 1.概要

Kintone(Cybozu) API で取得できるJSONデータを変換しデータベースへ登録できるアプリです。

基本はKintoneアプリのテーブル(サブテーブル)もデータベースにしてSQLで操作する目的となりますが、

Kintone API で取得できるデータは基本全てデータベース化できます。


## 2.注意点

Kintone API の取得(GET)のみの対応で、追加更新(PUT)はできません。

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

