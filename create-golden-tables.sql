/*

g brooks    4/12/2023

script to build special tables for managing oracle-specifc rules

*/

-- T_D_RULES_GOLDEN

    begin
        execute immediate 'drop table T_D_RULES_GOLDEN';
        exception when others then null;
    end;
    /
    CREATE TABLE "WERCS"."T_D_RULES_GOLDEN" 
    (	"F_RULE_NUMBER" NUMBER(8,0) NOT NULL ENABLE, 
        "F_FORMAT_FROM" CHAR(3 BYTE), 
        "F_SUBSECTION_A" VARCHAR2(8 BYTE), 
        "F_SUBSECTION_B" VARCHAR2(8 BYTE), 
        "F_SUBSECTION_C" VARCHAR2(8 BYTE), 
        "F_SUBSECTION_D" VARCHAR2(8 BYTE), 
        "F_FORMAT_TO" CHAR(3 BYTE), 
        "F_SUBSECTION_ID_TO" VARCHAR2(8 BYTE), 
        "F_RULE_NAME" VARCHAR2(255 BYTE), 
        "F_DATE_CREATED" DATE, 
        "F_DATE_APPLIED" DATE, 
        "F_USER_UPDATED" VARCHAR2(15 BYTE), 
        "F_PREFIXTYPE" NUMBER(*,0), 
        "F_CALC" NCLOB, 
        "F_DESC" NCLOB, 
        CONSTRAINT "PKRULEDGOLDEN" PRIMARY KEY ("F_RULE_NUMBER")
    USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS NOLOGGING 
    STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
    BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSIDX"  ENABLE	
    ) SEGMENT CREATION IMMEDIATE 
    PCTFREE 10 PCTUSED 50 INITRANS 10 MAXTRANS 255 NOCOMPRESS NOLOGGING
    STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
    BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSDATA" 
    LOB ("F_CALC") STORE AS BASICFILE (
    TABLESPACE "WERCSDATA" ENABLE STORAGE IN ROW CHUNK 8192 RETENTION NOCACHE NOLOGGING 
    STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
    BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)) 
    LOB ("F_DESC") STORE AS BASICFILE (
    TABLESPACE "WERCSDATA" ENABLE STORAGE IN ROW CHUNK 8192 RETENTION NOCACHE NOLOGGING 
    STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
    BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT));
    /

  -- T_RULES_GOLDEN

    begin
        execute immediate 'drop table T_RULES_GOLDEN';
        exception when others then null;
    end; 
    /
    CREATE TABLE "WERCS"."T_RULES_GOLDEN" 
    (	"F_RULE_NUMBER" NUMBER(8,0) NOT NULL ENABLE, 
        "F_RULE_NAME" VARCHAR2(255 BYTE), 
        "F_RULE_TYPE" VARCHAR2(5 BYTE), 
        "F_DATE_APPLIED" DATE, 
        "F_USER_UPDATED" VARCHAR2(15 BYTE), 
        "F_DATE_STAMP" DATE, 
        "F_GLOBAL_ID" VARCHAR2(50 BYTE), 
        "F_SCOPE" NUMBER(*,0) DEFAULT 0, 
        "F_COMMENTS" NVARCHAR2(2000), 
        CONSTRAINT "PKRULEGOLDEN" PRIMARY KEY ("F_RULE_NUMBER")
    USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS NOLOGGING 
    STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
    BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSIDX"  ENABLE
    ) SEGMENT CREATION IMMEDIATE 
    PCTFREE 10 PCTUSED 50 INITRANS 10 MAXTRANS 255 
    NOCOMPRESS NOLOGGING
    STORAGE(INITIAL 131072 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
    BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSDATA" ;
    /

  -- T_RULE_GROUPS_GOLDEN

    begin
        execute immediate 'drop table T_RULE_GROUPS_GOLDEN';
        exception when others then null;
    end;
    /
        CREATE TABLE "WERCS"."T_RULE_GROUPS_GOLDEN" 
    (	"F_GROUP_ID" NUMBER(8,0) NOT NULL ENABLE, 
        "F_GROUP_NAME" VARCHAR2(255 BYTE), 
        "F_GROUP_DESC" VARCHAR2(4000 BYTE), 
        "F_EXIT_ON_FIRST_FAIL" NUMBER(2,0), 
        "F_NEXT_GROUP_ON_FAIL" NUMBER(10,0), 
        "F_NEXT_GROUP_ON_SUCCESS" NUMBER(10,0), 
        "F_USER_UPDATED" VARCHAR2(15 BYTE), 
        "F_DATE_STAMP" DATE, 
        "F_GLOBAL_ID" VARCHAR2(50 BYTE), 
        "F_SCOPE" NUMBER(*,0) DEFAULT 0, 
        CONSTRAINT "PKRUULEGROUPSGOLDEN" PRIMARY KEY ("F_GROUP_ID")
    USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS NOLOGGING 
    STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
    BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSIDX"  ENABLE
    ) SEGMENT CREATION IMMEDIATE 
    PCTFREE 10 PCTUSED 50 INITRANS 10 MAXTRANS 255 NOCOMPRESS NOLOGGING
    STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSDATA" ;
    /

-- T_GROUP_MEMBERS_GOLDEN

    begin
        execute immediate 'drop table T_GROUP_MEMBERS_GOLDEN';
        exception when others then null;
    end;
    /
    CREATE TABLE "WERCS"."T_GROUP_MEMBERS_GOLDEN" 
    (	"F_GROUP_ID" NUMBER(8,0) NOT NULL ENABLE, 
        "F_RULE_NUMBER" NUMBER(8,0) NOT NULL ENABLE, 
        "F_RULE_DESC" NVARCHAR2(255), 
        "F_RULE_TYPE" VARCHAR2(1 BYTE), 
        "F_DATE_STAMP" DATE, 
        "F_USER_UPDATED" VARCHAR2(15 BYTE), 
        "F_SCOPE" NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE, 
        CONSTRAINT "PKGROUPMEMBERSGOLDEN" PRIMARY KEY ("F_GROUP_ID", "F_RULE_NUMBER")
    USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS NOLOGGING 
    STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
    BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSIDX"  ENABLE
    ) SEGMENT CREATION IMMEDIATE 
    PCTFREE 10 PCTUSED 50 INITRANS 10 MAXTRANS 255 NOCOMPRESS NOLOGGING
    STORAGE(INITIAL 131072 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSDATA" ;
    /

  -- T_RULE_STREAMS_GOLDEN

    begin
        execute immediate 'drop table T_RULE_STREAMS_GOLDEN';
        exception when others then null;
    end;
    /  
    CREATE TABLE "WERCS"."T_RULE_STREAMS_GOLDEN" 
    (	"F_STREAM_ID" NUMBER(10,0) NOT NULL ENABLE, 
        "F_NAME" VARCHAR2(255 BYTE), 
        "F_ACTIVE" NUMBER(1,0), 
        "F_DATE_STAMP" DATE, 
        "F_USER_UPDATED" VARCHAR2(15 BYTE), 
        "F_GUID" VARCHAR2(50 BYTE) DEFAULT UPPER(SYS_GUID()) NOT NULL ENABLE, 
        "F_COMMENTS" VARCHAR2(4000 BYTE), 
        CONSTRAINT "PKRULESTREAMSGOLDEN" PRIMARY KEY ("F_STREAM_ID")
    USING INDEX PCTFREE 10 INITRANS 10 MAXTRANS 255 COMPUTE STATISTICS 
    STORAGE(INITIAL 516096 NEXT 516096 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
    BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSIDX"  ENABLE
    ) SEGMENT CREATION IMMEDIATE 
    PCTFREE 10 PCTUSED 50 INITRANS 10 MAXTRANS 255 NOCOMPRESS NOLOGGING
    STORAGE(INITIAL 1048576 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSDATA" ;
    /

-- T_RULE_STREAM_MEMBERS_GOLDEN

    begin
        execute immediate 'drop table T_RULE_STREAM_MEMBERS_GOLDEN';
        exception when others then null;
    end;  
    /
    CREATE TABLE "WERCS"."T_RULE_STREAM_MEMBERS_GOLDEN" 
    (	"F_RECORD_ID" NUMBER(10,0) NOT NULL ENABLE, 
        "F_STREAM_ID" NUMBER NOT NULL ENABLE, 
        "F_GROUP_ID" NUMBER NOT NULL ENABLE, 
        "F_DATE_STAMP" DATE, 
        "F_USER_UPDATED" VARCHAR2(15 BYTE), 
        "F_GUID" VARCHAR2(50 BYTE) DEFAULT UPPER(SYS_GUID()) NOT NULL ENABLE, 
        "F_ORDER" NUMBER(10,0) DEFAULT 0, 
        CONSTRAINT "PKRULESTREAMMEMBERSGOLDEN" PRIMARY KEY ("F_RECORD_ID")
    USING INDEX PCTFREE 10 INITRANS 10 MAXTRANS 255 COMPUTE STATISTICS 
    STORAGE(INITIAL 516096 NEXT 516096 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
    BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSIDX"  ENABLE
    ) SEGMENT CREATION IMMEDIATE 
    PCTFREE 10 PCTUSED 50 INITRANS 10 MAXTRANS 255 NOCOMPRESS NOLOGGING
    STORAGE(INITIAL 1048576 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "WERCSDATA" ;
    /