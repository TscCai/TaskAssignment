/*
Navicat SQLite Data Transfer

Source Server         : TaskAssignment.Debug
Source Server Version : 30714
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30714
File Encoding         : 65001

Date: 2018-01-22 18:03:56
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
-- Table structure for Location
-- ----------------------------
DROP TABLE IF EXISTS "main"."Location";
CREATE TABLE "Location" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"LocationName"  TEXT(10) NOT NULL
);

-- ----------------------------
-- Table structure for Members
-- ----------------------------
DROP TABLE IF EXISTS "main"."Members";
CREATE TABLE "Members" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Name"  TEXT(10) NOT NULL,
"Enable"  BIT NOT NULL DEFAULT 1,
"IsInternal"  BIT NOT NULL DEFAULT 1,
"Countable"  BIT NOT NULL DEFAULT 0
);

-- ----------------------------
-- Table structure for sqlite_sequence
-- ----------------------------
DROP TABLE IF EXISTS "main"."sqlite_sequence";
CREATE TABLE sqlite_sequence(name,seq);

-- ----------------------------
-- Table structure for Substations
-- ----------------------------
DROP TABLE IF EXISTS "main"."Substations";
CREATE TABLE "Substations" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Voltage"  INTEGER NOT NULL,
"SubstationName"  TEXT(10) NOT NULL,
"LocationId"  INTEGER NOT NULL,
CONSTRAINT "FK1" FOREIGN KEY ("LocationId") REFERENCES "Location" ("Id"),
CONSTRAINT "NameUnique" UNIQUE ("SubstationName" ASC)
);

-- ----------------------------
-- Table structure for TaskCondition
-- ----------------------------
DROP TABLE IF EXISTS "main"."TaskCondition";
CREATE TABLE "TaskCondition" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"ConditionName"  TEXT(10) NOT NULL
);

-- ----------------------------
-- Table structure for Tasks
-- ----------------------------
DROP TABLE IF EXISTS "main"."Tasks";
CREATE TABLE "Tasks" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"SubstationId"  INTEGER NOT NULL,
"Content"  TEXT NOT NULL,
"Date"  datetime NOT NULL,
"TypeId"  INTEGER NOT NULL,
"ConditionId"  INTEGER NOT NULL DEFAULT 3,
"Visible"  Bit NOT NULL DEFAULT 1,
CONSTRAINT "FK1" FOREIGN KEY ("TypeId") REFERENCES "TaskType" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT,
CONSTRAINT "FK2" FOREIGN KEY ("ConditionId") REFERENCES "TaskCondition" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT,
CONSTRAINT "FK3" FOREIGN KEY ("SubstationId") REFERENCES "Substations" ("Id") ON DELETE RESTRICT ON UPDATE RESTRICT
);

-- ----------------------------
-- Table structure for TaskType
-- ----------------------------
DROP TABLE IF EXISTS "main"."TaskType";
CREATE TABLE "TaskType" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"TypeName"  TEXT NOT NULL
);
