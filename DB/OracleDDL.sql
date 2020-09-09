﻿/* TABLES */
BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"__EFMigrationsHistory" (
    "MigrationId" NVARCHAR2(150) NOT NULL,
    "ProductVersion" NVARCHAR2(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
)';
END;
EXCEPTION
WHEN OTHERS THEN
    IF(SQLCODE != -942)THEN
        RAISE;
    END IF;
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"ACCESS_LOG" (
    "ID" NUMBER(19) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "APPLICATION_TYPE" NUMBER(10) NOT NULL,
    "CHANNEL" VARCHAR2(20),
    "ACTION" VARCHAR2(80),
    "USERNAME" VARCHAR2(20),
    "GRANTED" NUMBER(1) NOT NULL,
    "ISSUED_DATE" DATE NOT NULL,
    CONSTRAINT "PK_ACCESS_LOG" PRIMARY KEY ("ID")
)
PARTITION BY RANGE(ISSUED_DATE) 
INTERVAL(NUMTOYMINTERVAL(1, ''MONTH'')) 
(  
   PARTITION ACCESS_LOG_P1 VALUES LESS THAN (TO_DATE(''01-07-2020'', ''DD-MM-YYYY''))
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"APPL_USER" (
    "USERNAME" VARCHAR2(20) NOT NULL,
    "DOMAIN" VARCHAR2(20),
    "FULLNAME" VARCHAR2(80),
    "ACCESS_TYPE" NUMBER(10) NOT NULL,
    CONSTRAINT "PK_APPL_USER" PRIMARY KEY ("USERNAME")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"AWARD_CHANNEL" (
    "ID" NUMBER(19) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "NAME" VARCHAR2(20),
    CONSTRAINT "PK_AWARD_CHANNEL" PRIMARY KEY ("ID")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"ISSUER_CHANNEL" (
    "ID" NUMBER(19) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "NAME" VARCHAR2(20),
    CONSTRAINT "PK_ISSUER_CHANNEL" PRIMARY KEY ("ID")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"PROMOTION" (
    "ID" NUMBER(19) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "CODE" VARCHAR2(40) NOT NULL,
	"NAME" VARCHAR2(60) NOT NULL,
    "ENABLED" NUMBER(1) NOT NULL,
    "VALID_FROM" DATE,
    "VALID_TO" DATE,
    "COUPON_SERIES" NUMBER(10) NOT NULL,
    CONSTRAINT "PK_PROMOTION" PRIMARY KEY ("ID")
)
PARTITION BY RANGE(id) INTERVAL(10)
  (PARTITION P1 VALUES LESS THAN(11))';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"PROPERTY" (
    "ID" NUMBER(19) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "NAME" VARCHAR2(20),
    CONSTRAINT "PK_PROPERTY" PRIMARY KEY ("ID")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"COUPON_SYSTEM" (
    "ID" NUMBER(19) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "NAME" VARCHAR2(20),
    "LOGIN" VARCHAR2(20),
    "PWD_HASH" VARCHAR2(200),
    CONSTRAINT "PK_COUPON_SYSTEM" PRIMARY KEY ("ID")
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"COUPON" (
    "ID" NUMBER(19) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "CODE" VARCHAR2(40),
    "HOLDER" VARCHAR2(20),
    "USER" VARCHAR2(20),
    "STATUS" NUMBER(10) NOT NULL,
    "AQUIRE_FROM" DATE,
    "AQUIRE_TO" DATE,
    "AWARD_FROM" DATE,
    "AWARD_TO" DATE,
    "PROMOTION_ID" NUMBER(19) NOT NULL,
    "COUPON_SERIES" NUMBER(10) NOT NULL,
    "MAX_REDEEM_NO" NUMBER(10) NOT NULL,
    "ENABLED" NUMBER(1) NOT NULL,
    CONSTRAINT "PK_COUPON" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_COUPON_PROMOTION_PROMOTION_ID" FOREIGN KEY ("PROMOTION_ID") REFERENCES "PROMOTION" ("ID") ON DELETE CASCADE
)
PARTITION BY REFERENCE(FK_COUPON_PROMOTION_PROMOTION_ID)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"PROMOTION_AWARD_CHANNEL" (
    "PROMOTION_ID" NUMBER(19) NOT NULL,
    "AWARD_CHANNEL_ID" NUMBER(19) NOT NULL,
    CONSTRAINT "PK_PROMOTION_AWARD_CHANNEL" PRIMARY KEY ("PROMOTION_ID", "AWARD_CHANNEL_ID"),
    CONSTRAINT "FK_PROMOTION_AWARD_CHANNEL_AWARD_CHANNEL_AWARD_CHANNEL_ID" FOREIGN KEY ("AWARD_CHANNEL_ID") REFERENCES "AWARD_CHANNEL" ("ID") ON DELETE CASCADE,
    CONSTRAINT "FK_PROMOTION_AWARD_CHANNEL_PROMOTION_PROMOTION_ID" FOREIGN KEY ("PROMOTION_ID") REFERENCES "PROMOTION" ("ID") ON DELETE CASCADE
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"PROMOTION_ISSUER_CHANNEL" (
    "PROMOTION_ID" NUMBER(19) NOT NULL,
    "ISSUER_CHANNEL_ID" NUMBER(19) NOT NULL,
    CONSTRAINT "PK_PROMOTION_ISSUER_CHANNEL" PRIMARY KEY ("PROMOTION_ID", "ISSUER_CHANNEL_ID"),
    CONSTRAINT "FK_PROMOTION_ISSUER_CHANNEL_ISSUER_CHANNEL_ISSUER_CHANNEL_ID" FOREIGN KEY ("ISSUER_CHANNEL_ID") REFERENCES "ISSUER_CHANNEL" ("ID") ON DELETE CASCADE,
    CONSTRAINT "FK_PROMOTION_ISSUER_CHANNEL_PROMOTION_PROMOTION_ID" FOREIGN KEY ("PROMOTION_ID") REFERENCES "PROMOTION" ("ID") ON DELETE CASCADE
)';
END;
/


BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"PROMOTION_PROPERTY" (
    "PROMOTION_ID" NUMBER(19) NOT NULL,
    "PROPERTY_ID" NUMBER(19) NOT NULL,
    CONSTRAINT "PK_PROMOTION_PROPERTY" PRIMARY KEY ("PROMOTION_ID", "PROPERTY_ID"),
    CONSTRAINT "FK_PROMOTION_PROPERTY_PROMOTION_PROMOTION_ID" FOREIGN KEY ("PROMOTION_ID") REFERENCES "PROMOTION" ("ID") ON DELETE CASCADE,
    CONSTRAINT "FK_PROMOTION_PROPERTY_PROPERTY_PROPERTY_ID" FOREIGN KEY ("PROPERTY_ID") REFERENCES "PROPERTY" ("ID") ON DELETE CASCADE
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"NOTIFY_LIST" (
    "CHANNEL_ID" NUMBER(19) NOT NULL,
    "SYSTEM_ID" NUMBER(19) NOT NULL,
    "URL" VARCHAR2(2000),
    CONSTRAINT "PK_NOTIFY_LIST" PRIMARY KEY ("SYSTEM_ID", "CHANNEL_ID"),
    CONSTRAINT "FK_NOTIFY_LIST_ISSUER_CHANNEL_CHANNEL_ID" FOREIGN KEY ("CHANNEL_ID") REFERENCES "ISSUER_CHANNEL" ("ID") ON DELETE CASCADE,
    CONSTRAINT "FK_NOTIFY_LIST_COUPON_SYSTEM_SYSTEM_ID" FOREIGN KEY ("SYSTEM_ID") REFERENCES "COUPON_SYSTEM" ("ID") ON DELETE CASCADE
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"COUPON_AWARD_CHANNEL" (
    "COUPON_ID" NUMBER(19) NOT NULL,
    "AWARD_CHANNEL_ID" NUMBER(19) NOT NULL,
    CONSTRAINT "PK_COUPON_AWARD_CHANNEL" PRIMARY KEY ("COUPON_ID", "AWARD_CHANNEL_ID"),
    CONSTRAINT "FK_COUPON_AWARD_CHANNEL_AWARD_CHANNEL_AWARD_CHANNEL_ID" FOREIGN KEY ("AWARD_CHANNEL_ID") REFERENCES "AWARD_CHANNEL" ("ID") ON DELETE CASCADE,
    CONSTRAINT "FK_COUPON_AWARD_CHANNEL_COUPON_COUPON_ID" FOREIGN KEY ("COUPON_ID") REFERENCES "COUPON" ("ID") ON DELETE CASCADE
)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"COUPON_HISTORY" (
    "ID" NUMBER(19) GENERATED BY DEFAULT ON NULL AS IDENTITY NOT NULL,
    "ISSUED_DATE" DATE NOT NULL,
    "ACTION" VARCHAR2(2000),
    "STATUS" NUMBER(10) NOT NULL,
    "COUPON_ID" NUMBER(19) NOT NULL,
    "USER" VARCHAR2(2000),
    CONSTRAINT "PK_COUPON_HISTORY" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_COUPON_HISTORY_COUPON_COUPON_ID" FOREIGN KEY ("COUPON_ID") REFERENCES "COUPON" ("ID") ON DELETE CASCADE
)
PARTITION BY REFERENCE(FK_COUPON_HISTORY_COUPON_COUPON_ID)';
END;
/

BEGIN 
EXECUTE IMMEDIATE 'CREATE TABLE 
"COUPON_ISSUER_CHANNEL" (
    "COUPON_ID" NUMBER(19) NOT NULL,
    "ISSUER_CHANNEL_ID" NUMBER(19) NOT NULL,
    CONSTRAINT "PK_COUPON_ISSUER_CHANNEL" PRIMARY KEY ("COUPON_ID", "ISSUER_CHANNEL_ID"),
    CONSTRAINT "FK_COUPON_ISSUER_CHANNEL_COUPON_COUPON_ID" FOREIGN KEY ("COUPON_ID") REFERENCES "COUPON" ("ID") ON DELETE CASCADE,
    CONSTRAINT "FK_COUPON_ISSUER_CHANNEL_ISSUER_CHANNEL_ISSUER_CHANNEL_ID" FOREIGN KEY ("ISSUER_CHANNEL_ID") REFERENCES "ISSUER_CHANNEL" ("ID") ON DELETE CASCADE
)';
END;
/


CREATE UNIQUE INDEX "IX_COUPON_CODE" ON "COUPON" ("CODE")
/

CREATE INDEX "IX_COUPON_PROMOTION_ID" ON "COUPON" ("PROMOTION_ID")
/

CREATE INDEX "IX_COUPON_AWARD_CHANNEL_AWARD_CHANNEL_ID" ON "COUPON_AWARD_CHANNEL" ("AWARD_CHANNEL_ID")
/

CREATE INDEX "IX_COUPON_HISTORY_COUPON_ID" ON "COUPON_HISTORY" ("COUPON_ID")
/

CREATE INDEX "IX_COUPON_ISSUER_CHANNEL_ISSUER_CHANNEL_ID" ON "COUPON_ISSUER_CHANNEL" ("ISSUER_CHANNEL_ID")
/

CREATE INDEX "IX_NOTIFY_LIST_CHANNEL_ID" ON "NOTIFY_LIST" ("CHANNEL_ID")
/

CREATE UNIQUE INDEX "IX_PROMOTION_CODE" ON "PROMOTION" ("CODE")  WHERE [CODE] IS NOT NULL
/

CREATE INDEX "IX_PROMOTION_AWARD_CHANNEL_AWARD_CHANNEL_ID" ON "PROMOTION_AWARD_CHANNEL" ("AWARD_CHANNEL_ID")
/

CREATE INDEX "IX_PROMOTION_ISSUER_CHANNEL_ISSUER_CHANNEL_ID" ON "PROMOTION_ISSUER_CHANNEL" ("ISSUER_CHANNEL_ID")
/

CREATE INDEX "IX_PROMOTION_PROPERTY_PROPERTY_ID" ON "PROMOTION_PROPERTY" ("PROPERTY_ID")
/

CREATE UNIQUE INDEX "IX_COUPON_SYSTEM_NAME" ON "COUPON_SYSTEM" ("NAME")
/


/* GRANTS */
BEGIN
  FOR a IN (SELECT TABLE_NAME FROM ALL_TABLES WHERE OWNER = 'APL_KUPON_MGMT')
  LOOP
    EXECUTE IMMEDIATE 'GRANT SELECT, INSERT, UPDATE ON "' || a.table_name || '" TO KUPON_MGMT_FULL';
    EXECUTE IMMEDIATE 'GRANT SELECT ON "' || a.table_name || '" TO KUPON_MGMT_READ';
  END LOOP;
  EXECUTE IMMEDIATE 'GRANT DELETE ON "SYSTEM" TO KUPON_MGMT_FULL';
  EXECUTE IMMEDIATE 'GRANT DELETE ON "NOTIFY_LIST" TO KUPON_MGMT_FULL';
  EXECUTE IMMEDIATE 'GRANT DELETE ON "PROMOTION_PROPERTY" TO KUPON_MGMT_FULL';
  EXECUTE IMMEDIATE 'GRANT DELETE ON "PROMOTION_ISSUER_CHANNEL" TO KUPON_MGMT_FULL';
  EXECUTE IMMEDIATE 'GRANT DELETE ON "PROMOTION_AWARD_CHANNEL" TO KUPON_MGMT_FULL';
END;
/

/* DONE */