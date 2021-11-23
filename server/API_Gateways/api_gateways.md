# Api Gateways

# Zuul

Is a gateway service that provides dynamic routing, monitoring, resiliency,  security, and more. It acts as the front door to Netflix’s server infrastructure, handling traffic from all Netflix users around the world. It also routes requests, supports developer testing and debugging, provides deep insight into Netflix’s overall service health, protects it from attacks, and channels traffic to other cloud regions when an AWS region is in trouble

https://github.com/Netflix/zuul


Architectural Overview
https://github.com/Netflix/zuul/wiki/How-It-Works-2.0

***Filters***
Filters are the core of Zuul's functionality. They are responsible for the business logic of the application and can perform a variety of tasks
https://github.com/Netflix/zuul/wiki/Filters

# Lura Project

An extendable, simple and stateless high-performance API Gateway framework designed for both cloud-native and on-prem setups.

Lura is presented as a Go library that you can include in your own Go application to build a powerful proxy or API gateway.


https://luraproject.org/



Ultra performant API Gateway with middlewares. A project hosted at The Linux Foundation 

- Lura is the KrakenD’s engine. Formerly known as “KrakenD framework” until we donated it to The Linux Foundation on 2021. It is not a product itself but a set of libraries.
- KrakenD is our open-source API Gateway ready to use
- KrakenD Enterprise is our commercial version, including services to businesses


https://www.krakend.io/


 KrakenD Community Edition. Make your binary of KrakenD API Gateway 
https://github.com/devopsfaith/krakend-ce