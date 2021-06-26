# Canvas

# Caracteristicas


**Rubricas.**

https://www.youtube.com/watch?v=11tQjRwZVQg



# Api REST

Canvas LMS API Documentation
https://canvas.instructure.com/doc/api/



**Object IDs, SIS IDs, and special IDs**

Recuperar un objeto con SIS
```
/api/v1/courses/sis_course_id:A1234/assignments
```

https://canvas.instructure.com/doc/api/file.object_ids.html


**Pagination**


La paginacion se utliza Link header:
https://canvas.instructure.com/doc/api/file.pagination.html


El servicio submissions la paginacion no tiene numero: 
page=bookmark:WzEwMjY4XQ&per_page=10>; rel="next"



# GraphQL API


https://canvas.instructure.com/doc/api/file.graphql.html


# Live Events

Live Events are specific events emitted by Canvas when an interesting action takes place, such as a page being accessed, a student submitting an assignment, or course settings being updated. Customers can subscribe to specific events and receive them using either an AWS SQS queue or an HTTPS Webhook

https://utpl.instructure.com/doc/api/file.data_service_introduction.html
