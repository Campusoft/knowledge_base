# Api REST


Activar

Site administration → Advanced features

- Activar la opcion "Enable web services"




## Paginacion

- API REST de Moodle puede manejar paginación, pero no todas las funciones la implementan de forma explícita. Depende de la función que estés utilizando. (Ejemplo)

Función REST | ¿Tiene paginación? | Parámetros
-- | -- | --
core_user_get_users | ✅ Sí | limitfrom, limitnumber
core_user_get_users_by_field | ❌ No | —
core_course_search_courses | ✅ Sí | page, perpage
core_enrol_get_enrolled_users | ❌ No | —
core_cohort_get_cohorts | ✅ Desde Moodle 3.8 | offset, limit
mod_assign_get_assignments | ❌ No | —


## Endpoint


**Usuarios**




**Cursos**


***core_course_get_courses_by_field***

- Get courses matching a specific field (id/s, shortname, idnumber, category)

Arguments
- field (Default to "")
  - The field to search can be left empty for all courses or: 
  - id: course id 
  - ids: comma separated course ids 
  - shortname: course short name 
  - idnumber: course id number 
  - category: category id the course belongs to 
  - sectionid: section id that belongs to a course
- value (Default to "") The value to match
  
Notas:  Al establecer category, se retorna los cursos que estan explicitamente bajo esa categoria, no incluye los cursos que estan en sub/categorias de la categoria indicada.

Ejemplo en POST (form-data)

```
POST /webservice/rest/server.php
Content-Type: application/x-www-form-urlencoded

wstoken=tu_token
wsfunction=core_course_get_courses_by_field
moodlewsrestformat=json
field=shortname
value=D0002
```



***core_course_search_courses***

Soporta Paginacion

Arguments

- criterianame (Required)
  - criteria name (search, modulelist (only admins), blocklist (only admins), tagid)
- criteriavalue 
  - criteria value
- page (Opcional)
  - La pagina que se requiere. Inicia desde 0
- perpage (Opcional)
  - Registros por pagina  
		
Limitaciones de core_course_search_courses

La búsqueda está limitada internamente al siguiente criterio:

```
WHERE course.fullname LIKE :criteriavalue
   OR course.shortname LIKE :criteriavalue
   OR course.idnumber LIKE :criteriavalue
   OR course.summary LIKE :criteriavalue
```


Campo | ¿Se busca?
-- | --
fullname | ✅ Sí
shortname | ✅ Sí
summary | ✅ Sí
idnumber | ✅ Sí
Campos personalizados (customfields) | ❌ No


Ejemplo en POST (form-data)

```
POST /webservice/rest/server.php
Content-Type: application/x-www-form-urlencoded

wstoken=tu_token
wsfunction=core_course_search_courses
moodlewsrestformat=json
criterianame=search
criteriavalue=historia
page=1
perpage=10
```


**Enrollamientos**





# Errores


Access control exception (Access to the function core_course_search_courses() is not allowed


Invalid parameter value detected (Invalid password: you must provide a password, or set createpassword.)



```
{"exception":"core\\exception\\moodle_exception","errorcode":"Message was not sent.","message":"error\/Message was not sent."}
```