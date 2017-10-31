--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.5
-- Dumped by pg_dump version 9.6.5

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

SET search_path = public, pg_catalog;

ALTER TABLE ONLY public.scenario DROP CONSTRAINT "scenario_statusid_scenario-status_id";
ALTER TABLE ONLY public.scenario DROP CONSTRAINT "scenario_resultstatusid_scenario-result-status_id";
ALTER TABLE ONLY public.scenario DROP CONSTRAINT scenario_projectid_project_id;
ALTER TABLE ONLY public.project DROP CONSTRAINT project_userid_user_id;
ALTER TABLE ONLY public.project DROP CONSTRAINT project_applicationid_application_id;
ALTER TABLE ONLY public."user" DROP CONSTRAINT user_pkey;
ALTER TABLE ONLY public."user" DROP CONSTRAINT user_email;
ALTER TABLE ONLY public.scenario DROP CONSTRAINT scenario_pkey;
ALTER TABLE ONLY public."scenario-status" DROP CONSTRAINT "scenario-status_pkey";
ALTER TABLE ONLY public."scenario-result-status" DROP CONSTRAINT "scenario-result-status_pkey";
ALTER TABLE ONLY public.project DROP CONSTRAINT project_pkey;
ALTER TABLE ONLY public.application DROP CONSTRAINT application_pkey;
ALTER TABLE public."user" ALTER COLUMN "Id" DROP DEFAULT;
ALTER TABLE public.scenario ALTER COLUMN "Id" DROP DEFAULT;
ALTER TABLE public.project ALTER COLUMN "Id" DROP DEFAULT;
ALTER TABLE public.application ALTER COLUMN "Id" DROP DEFAULT;
DROP SEQUENCE public."user_Id_seq";
DROP TABLE public."user";
DROP SEQUENCE public."scenario_Id_seq";
DROP TABLE public."scenario-status";
DROP TABLE public."scenario-result-status";
DROP TABLE public.scenario;
DROP SEQUENCE public."project_Id_seq";
DROP TABLE public.project;
DROP SEQUENCE public."application_Id_seq";
DROP TABLE public.application;
DROP EXTENSION plpgsql;
DROP SCHEMA public;
--
-- Name: public; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA public;


--
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA public IS 'standard public schema';


--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: application; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE application (
    "Id" integer NOT NULL,
    "Name" character varying NOT NULL,
    "Image" bytea,
    "ModelFile" bytea,
    "ModelFilename" character varying,
    "Notes" text,
    "IsArchived" boolean NOT NULL,
    "DateCreated" timestamp without time zone NOT NULL,
    "DateModified" timestamp without time zone NOT NULL
);


--
-- Name: application_Id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE "application_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- Name: application_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE "application_Id_seq" OWNED BY application."Id";


--
-- Name: project; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE project (
    "Id" integer NOT NULL,
    "ApplicationId" integer NOT NULL,
    "UserId" integer NOT NULL,
    "Name" character varying NOT NULL,
    "Image" bytea,
    "Description" text,
    "IsArchived" boolean NOT NULL,
    "DateCreated" timestamp without time zone NOT NULL,
    "DateModified" timestamp without time zone NOT NULL
);


--
-- Name: project_Id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE "project_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- Name: project_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE "project_Id_seq" OWNED BY project."Id";


--
-- Name: scenario; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE scenario (
    "Id" integer NOT NULL,
    "ProjectId" integer NOT NULL,
    "Name" character varying NOT NULL,
    "StatusId" integer NOT NULL,
    "ResultStatusId" integer NOT NULL,
    "Description" text,
    "WarmUpPeriod" timestamp without time zone,
    "StartDate" timestamp without time zone,
    "EndDate" timestamp without time zone,
    "Replications" integer,
    "RandomSkip" integer,
    "IsArchived" boolean NOT NULL,
    "ExperimentReference" character varying,
    "DateCreated" timestamp without time zone NOT NULL,
    "DateModified" timestamp without time zone NOT NULL
);


--
-- Name: scenario-result-status; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE "scenario-result-status" (
    "Id" integer NOT NULL,
    "Name" character varying NOT NULL
);


--
-- Name: scenario-status; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE "scenario-status" (
    "Id" integer NOT NULL,
    "Name" character varying NOT NULL
);


--
-- Name: scenario_Id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE "scenario_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- Name: scenario_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE "scenario_Id_seq" OWNED BY scenario."Id";


--
-- Name: user; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE "user" (
    "Id" integer NOT NULL,
    "Email" character varying(256) NOT NULL,
    "Name" character varying(256) NOT NULL
);


--
-- Name: user_Id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE "user_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- Name: user_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE "user_Id_seq" OWNED BY "user"."Id";


--
-- Name: application Id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY application ALTER COLUMN "Id" SET DEFAULT nextval('"application_Id_seq"'::regclass);


--
-- Name: project Id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY project ALTER COLUMN "Id" SET DEFAULT nextval('"project_Id_seq"'::regclass);


--
-- Name: scenario Id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY scenario ALTER COLUMN "Id" SET DEFAULT nextval('"scenario_Id_seq"'::regclass);


--
-- Name: user Id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY "user" ALTER COLUMN "Id" SET DEFAULT nextval('"user_Id_seq"'::regclass);


--
-- Name: application application_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY application
    ADD CONSTRAINT application_pkey PRIMARY KEY ("Id");


--
-- Name: project project_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY project
    ADD CONSTRAINT project_pkey PRIMARY KEY ("Id");


--
-- Name: scenario-result-status scenario-result-status_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY "scenario-result-status"
    ADD CONSTRAINT "scenario-result-status_pkey" PRIMARY KEY ("Id");


--
-- Name: scenario-status scenario-status_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY "scenario-status"
    ADD CONSTRAINT "scenario-status_pkey" PRIMARY KEY ("Id");


--
-- Name: scenario scenario_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY scenario
    ADD CONSTRAINT scenario_pkey PRIMARY KEY ("Id");


--
-- Name: user user_email; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY "user"
    ADD CONSTRAINT user_email UNIQUE ("Email");


--
-- Name: user user_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY "user"
    ADD CONSTRAINT user_pkey PRIMARY KEY ("Id");


--
-- Name: project project_applicationid_application_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY project
    ADD CONSTRAINT project_applicationid_application_id FOREIGN KEY ("ApplicationId") REFERENCES application("Id");


--
-- Name: project project_userid_user_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY project
    ADD CONSTRAINT project_userid_user_id FOREIGN KEY ("UserId") REFERENCES "user"("Id");


--
-- Name: scenario scenario_projectid_project_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY scenario
    ADD CONSTRAINT scenario_projectid_project_id FOREIGN KEY ("ProjectId") REFERENCES project("Id");


--
-- Name: scenario scenario_resultstatusid_scenario-result-status_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY scenario
    ADD CONSTRAINT "scenario_resultstatusid_scenario-result-status_id" FOREIGN KEY ("ResultStatusId") REFERENCES "scenario-result-status"("Id");


--
-- Name: scenario scenario_statusid_scenario-status_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY scenario
    ADD CONSTRAINT "scenario_statusid_scenario-status_id" FOREIGN KEY ("StatusId") REFERENCES "scenario-status"("Id");


--
-- Name: public; Type: ACL; Schema: -; Owner: -
--

GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

