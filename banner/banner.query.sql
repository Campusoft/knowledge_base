--- Query Banner

-----------------------------------------------
---------- PERSON
-----------------------------------------------

select SPBPERS_SSN, spriden.* from spriden, SPBPERS
where
    1=1
    and SPBPERS_pidm = spriden_pidm
    and spriden_id = 'U00002021'
;


--Obtener GUID de Personas 
select 
       gorguid_guid,
       spriden_id,
       spriden_pidm,
       (select SPBPERS_SSN from SPBPERS
       where 
        SPBPERS_pidm = spriden_pidm) SPBPERS_SSN
from 
    gorguid , spriden
where
    gorguid_domain_key = spriden_pidm
    and gorguid_ldm_name = 'persons'
    and gorguid_guid = '3f0daace-cab5-4523-9dfe-b2835d12cdec'
--    and  SPBPERS_SSN in ('1721346318')
--    and spriden_id = 'U00002021'
;

select 
       gorguid_guid,
       spriden_id,
       spriden_pidm,
       SPBPERS_SSN
from 
    gorguid , spriden, SPBPERS
where
    gorguid_domain_key = spriden_pidm
    and gorguid_ldm_name = 'persons'
    and  SPBPERS_pidm = spriden_pidm
--    and gorguid_guid = '3f0daace-cab5-4523-9dfe-b2835d12cdec'
   and  SPBPERS_SSN in ('CRMTEST100')
--    and spriden_id = 'CRMTEST100'
;
  

--3f0daace-cab5-4523-9dfe-b2835d12cdec


-----------------------------------------------
---------- PERIODOS ACADEMICOS
-----------------------------------------------


--Periodo GUID
select
    sfrrgid_guid,sfrrgid_pidm,sfrrgid_term_code, sfrrgid_crn,
    term_guid.gorguid_guid periodo_guid
from 
    sfrrgid,  gorguid term_guid
    --,SFRSTCR
where
    --SFRSTCR_PIDM = sfrrgid_pidm
    --and SFRSTCR_TERM_CODE = sfrrgid_term_code
    --and SFRSTCR_CRN = sfrrgid_crn
    --and 
    term_guid.gorguid_ldm_name = 'term'
    and term_guid.gorguid_domain_key = sfrrgid_term_code
    --and sfrrgid_pidm='<enter your pidm here' 
    --and sfrrgid_term_code='<enter your term code here>' 
    --and sfrrgid_crn='<enter your crn here>'
    and sfrrgid_guid = '22f2925b-14bc-4701-9fc6-595a2605ab30' 
    --and sfrrgid_guid in(:GUID_LIST);   
;

-----------------------------------------------
---------- Admission Applications
-----------------------------------------------

--Caso
--Crear formulario ligero, luego replicar admisiones API
--Identificacion: CRMTEST100
--Clave: Demo.2020! Usuario: cverificacion 
--Baner: U00297165
--GUID Person: 8fcc89f7-59ff-4449-8c73-5681bb722036
--GUID Admision: f17f3b84-4af8-45cc-953d-2cc6d86cd20e

select adm.* from SARADAP adm, spriden
where
    1=1
    and SARADAP_PIDM = spriden_pidm
    and spriden_id = 'U00297165'
;

select * from SORLCUR
where
    SORLCUR_PIDM  = gb_common.f_get_pidm('U00297165')
;

select * from SORLFOS
where
    SORLFOS_PIDM  = gb_common.f_get_pidm('U00297165')
;

--SARADAP_GUID
select * from SVQ_ADMISSION_APPLICATION
where
    SARADAP_PIDM = gb_common.f_get_pidm('U00297165')
;


select * from SVQ_ADM_APPLICATION
where
    SPRIDEN_GUID = 
    (select 
           gorguid_guid 
    from 
        gorguid , spriden
    where
        gorguid_domain_key = spriden_pidm
        and gorguid_ldm_name = 'persons'
    --    and gorguid_guid = '3f0daace-cab5-4523-9dfe-b2835d12cdec'
    --    and  SPBPERS_SSN in ('1721346318')
        and spriden_id = 'U00002021')
;

 
select * from SVQ_ADM_DECISION
where
    SARADAP_GUID = (select SARADAP_GUID from SVQ_ADMISSION_APPLICATION
    where
        SARADAP_PIDM = gb_common.f_get_pidm('U00297165')
)
; 


select * from SVQ_ADM_APPL_SUPP_ITEM
where
    SARADAP_GUID= (select SARADAP_GUID from SVQ_ADMISSION_APPLICATION
    where
        SARADAP_PIDM = gb_common.f_get_pidm('U00297165')
)
;

--Estructuras Admisiones

--admisiones
select adm.* from SARADAP adm, spriden
where
    1=1
    and SARADAP_PIDM = spriden_pidm
    and spriden_id = 'U00297165'
;

select * from STVSTYP
;


select * from SVQ_ADM_APPLICATION
;

select * from SVQ_ADMISSION_APPLICATION
where
    SARADAP_PIDM = gb_common.f_get_pidm('U00297165')
;


select * from SVQ_ADM_POPULATION
;

-----------------------
--decisiones
--------------------

--tipos decisiones
select * from stvapdc
;

--decisiones
select * from sarappd
;

  SELECT sarappd_surrogate_id,
        sarappd_version,
        sarappd_guid,
        sarappd_apdc_date,
        saradap_guid,
        stvapdc_guid,
        stvapdc_inst_acc_ind,
        stvapdc_stdn_acc_ind,
        stvapdc_appl_inact,
        stvapdc_reject_ind
 FROM sarappd, saradap, stvapdc
 WHERE sarappd_pidm = saradap_pidm
 AND sarappd_term_code_entry = saradap_term_code_entry
 AND sarappd_appl_no = saradap_appl_no
 AND sarappd_apdc_code = stvapdc_code
 ;
 
 select * from SVQ_ADM_DECISION
where
    SARADAP_GUID = (select SARADAP_GUID from SVQ_ADMISSION_APPLICATION
    where
        SARADAP_PIDM = gb_common.f_get_pidm('U00297165')
)
; 
 
 
-----------------------
--Requisitos
--------------------
select * 
FROM sarchkl, saradap, stvadmr
 WHERE sarchkl_pidm = saradap_pidm
 AND sarchkl_term_code_entry = saradap_term_code_entry
 AND sarchkl_appl_no = saradap_appl_no
 AND sarchkl_admr_code = stvadmr_code
;

select * from SVQ_ADM_APPL_SUPP_ITEM
where
    SARADAP_GUID= (select SARADAP_GUID from SVQ_ADMISSION_APPLICATION
    where
        SARADAP_PIDM = gb_common.f_get_pidm('U00297165')
)
;

select * from STVORIG
;

select * from SVQ_SOURCES
;

--Admissions Application Repeating Table
select * from all_tab_comments
where
    table_name = 'SARADAP'
;

--Originator Validation Table
select * from all_tab_comments
where
    table_name = 'STVORIG'
;

--Student Type Validation Table
select * from all_tab_comments
where
    table_name = 'STVSTYP'
;


---------------------
--- INFRAESTRUCTURA
-------------------

--Edificiones
select * from stvBLDG
;

select * from SVQ_SLBRDEF_SLBBLDG
;

select * from NOSTU_SVQ_SLBRDEF_SLBBLDG
;

select * from SLBRDEF
;

select * from SLRRASG
;

--SLBRDEF	TABLE	Room Description Table
--SLBBLDG	TABLE	Location/Building Description Table
select * from SLBBLDG
;



--Caracteristicas aulas
select * from stvrdef
;

select * from SVQ_ROOM_ATTRIBUTE
;


select * from SLRLMFE
;

select * from slrrmat
;

select * from all_tab_comments
where
    table_name like  'SL%'
;

select * from goriccr
;

select * from SVQ_ROOM_RATE
;

--room-characteristics	SVQ_ROOM_ATTRIBUTE
--room-rates	SVQ_ROOM_RATE

--buildings	NOSTU_SVQ_SLBRDEF_SLBBLDG
--buildings	SVQ_SLBRDEF_SLBBLDG

