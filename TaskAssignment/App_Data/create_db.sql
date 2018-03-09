/*
Navicat SQLite Data Transfer

Source Server         : TaskAssignment.Debug
Source Server Version : 30714
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30714
File Encoding         : 65001

Date: 2018-03-08 12:45:09
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
"IsLeader"  BIT NOT NULL DEFAULT 0,
CONSTRAINT "FK1" FOREIGN KEY ("TaskId") REFERENCES "Tasks" ("Id") ON DELETE CASCADE,
CONSTRAINT "FK2" FOREIGN KEY ("MemberId") REFERENCES "Members" ("Id") ON DELETE CASCADE
);

-- ----------------------------
-- Table structure for Attendance
-- ----------------------------
DROP TABLE IF EXISTS "main"."Attendance";
CREATE TABLE "Attendance" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"MemberId"  INTEGER NOT NULL,
"TaskId"  INTEGER,
"TypeId"  INTEGER NOT NULL,
"StartDate"  DATETIME NOT NULL,
"FinishDate"  DATETIME NOT NULL,
"Comments"  TEXT(255),
CONSTRAINT "FK1" FOREIGN KEY ("MemberId") REFERENCES "Members" ("Id") ON DELETE CASCADE,
CONSTRAINT "FK2" FOREIGN KEY ("TypeId") REFERENCES "AttendanceType" ("Id") ON DELETE CASCADE,
CONSTRAINT "FK3" FOREIGN KEY ("TaskId") REFERENCES "Tasks" ("Id") ON DELETE CASCADE
);

-- ----------------------------
-- Table structure for AttendanceType
-- ----------------------------
DROP TABLE IF EXISTS "main"."AttendanceType";
CREATE TABLE "AttendanceType" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"TypeName"  TEXT(10) NOT NULL DEFAULT 10,
"IsAbsent"  BIT NOT NULL DEFAULT 0,
"Alias"  TEXT(15) NOT NULL,
"Symbol"  TEXT(4)
);

-- ----------------------------
-- Table structure for Holidays
-- ----------------------------
DROP TABLE IF EXISTS "main"."Holidays";
CREATE TABLE "Holidays" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Year"  INTEGER NOT NULL,
"Holidays"  TEXT(255) NOT NULL,
"ExtraWorkdays"  TEXT(255) NOT NULL,
CONSTRAINT "Year_Uni" UNIQUE ("Year")
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
CONSTRAINT "FK1" FOREIGN KEY ("LocationId") REFERENCES "Location" ("Id") ON DELETE CASCADE,
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
CONSTRAINT "FK1" FOREIGN KEY ("TypeId") REFERENCES "TaskType" ("Id") ON DELETE CASCADE ON UPDATE RESTRICT,
CONSTRAINT "FK2" FOREIGN KEY ("ConditionId") REFERENCES "TaskCondition" ("Id") ON DELETE CASCADE ON UPDATE RESTRICT,
CONSTRAINT "FK3" FOREIGN KEY ("SubstationId") REFERENCES "Substations" ("Id") ON DELETE CASCADE ON UPDATE RESTRICT
);

-- ----------------------------
-- Table structure for TaskType
-- ----------------------------
DROP TABLE IF EXISTS "main"."TaskType";
CREATE TABLE "TaskType" (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"TypeName"  TEXT NOT NULL
);

-- ----------------------------
-- Triggers structure for table Assign
-- ----------------------------
DROP TRIGGER IF EXISTS "main"."DelAttCascade_Trigg";
DELIMITER ;;
CREATE TRIGGER "DelAttCascade_Trigg" AFTER DELETE ON "Assign"
BEGIN
  /* Trigger action (UPDATE, INSERT, DELETE or SELECT statements) goes here. */
	DELETE FROM Attendance WHERE TaskId=old.TaskId and MemberId=old.MemberId;
END
;;
DELIMITER ;
