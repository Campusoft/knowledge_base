# npm


# yarn

Download install windows. (*.msi) de los diferentes realeases
https://github.com/yarnpkg/yarn/releases


yarn dlx es el equivalente de npx pero para Yarn 2+ (Berry).
(DLX = Download and eXecute).


# PnpM

Fast, disk space efficient package manager

PNPM: PNPM is 3 times faster and more efficient than NPM.  With both cold and hot cache, PNPM is faster than Yarn. 


Ventajas clave:

- Usa un único almacenamiento global de dependencias (“content-addressable store”).
- Las dependencias se vinculan (symlink) a cada proyecto, sin duplicarlas.
- Mucho más rápido para instalar y actualizar.
- Soporta workspaces de forma nativa → ideal para monorepos.



npm command	pnpm equivalent
npm install	pnpm install
npm i <pkg>	pnpm add <pkg>
npm run <cmd>	pnpm <cmd>


https://pnpm.io/


¿Qué es un pnpm workspace?

Un workspace es una forma de manejar varios proyectos (paquetes) dentro de un mismo repositorio (monorepo).
