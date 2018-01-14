/*
Navicat SQLite Data Transfer

Source Server         : TaskAssignment.Debug
Source Server Version : 30714
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30714
File Encoding         : 65001

Date: 2018-01-14 17:40:42
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for Assign
-- ----------------------------
DROP TABLE IF EXISTS "main"."Assign";
CREATE TABLE "Assign" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"TaskId"  INTEGER NOT NULL,
"MemberId"  INTEGER NOT NULL,
CONSTRAINT "FK1" FOREIGN KEY ("TaskId") REFERENCES "Tasks" ("Id"),
CONSTRAINT "FK2" FOREIGN KEY ("MemberId") REFERENCES "Members" ("Id")
);

-- ----------------------------
-- Table structure for Members
-- ----------------------------
DROP TABLE IF EXISTS "main"."Members";
CREATE TABLE "Members" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Name"  TEXT NOT NULL,
"Enable"  bit NOT NULL DEFAULT 1,
"IsInternal"  Bit NOT NULL DEFAULT 1
);

-- ----------------------------
-- Table structure for sqlite_sequence
-- ----------------------------
DROP TABLE IF EXISTS "main"."sqlite_sequence";
CREATE TABLE sqlite_sequence(name,seq);

-- ----------------------------
-- Table structure for Tasks
-- ----------------------------
DROP TABLE IF EXISTS "main"."Tasks";
CREATE TABLE "Tasks" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Content"  TEXT NOT NULL,
"Date"  datetime NOT NULL,
"TypeId"  INTEGER NOT NULL,
CONSTRAINT "FK1" FOREIGN KEY ("TypeId") REFERENCES "TaskType" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT
);

-- ----------------------------
-- Table structure for TaskType
-- ----------------------------
DROP TABLE IF EXISTS "main"."TaskType";
CREATE TABLE "TaskType" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"TypeName"  TEXT NOT NULL
);
