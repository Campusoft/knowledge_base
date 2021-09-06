# Nebular
- Nebular is a customizable Angular UI library that contains 40+ UI components, four visual themes, and Auth and Security modules. Recognized at the prestigious AngularConnect 2018, this Angular framework allows focusing on beautiful designs to adapt them to your brand. Nebular is free of charge and open-source. 
- https://akveo.github.io/nebular/
## Instalación
1. Crear app con routing y css
2. Instalar paquetes básicos de nebular. 
	- npm install --save @nebular/theme @angular/cdk @angular/animations
3. Instalar paquete de iconos.
	- npm install --save eva-icons @nebular/eva-icons 
	- yarn add eva-icons @nebular/eva-icons
4. Agregar módulo al app.module, íconos y estilos a angular.json
```
AppModule.ts
import { NbThemeModule } from '@nebular/theme';

@NgModule({
  imports: [
    ...
    // this will enable the default theme, you can change this by passing `{ name: 'dark' }` to enable the dark theme
    NbThemeModule.forRoot(),
	NbIconModule
  ]
})
export class AppModule {

angular.json
"styles": [
  "node_modules/@nebular/theme/styles/prebuilt/default.css", // or dark.css
  "node_modules/nebular-icons/scss/nebular-icons.scss",
],
```
5. Crear una nueva página utilizando componentes nebular
- https://akveo.github.io/nebular/docs/guides/create-nebular-page#create-nebular-page
- Se requiere importar el módulo correspondiente a cada componente nebular, se recomienda crear un módulo aparte para no complicar el app.module
```
AppModule.ts
import { NbThemeModule, NbSidebarModule, NbLayoutModule, NbButtonModule, NbThemeService, NbSidebarService, NbCardModule } from '@nebular/theme';

  imports: [
    NbLayoutModule,
    NbSidebarModule, // NbSidebarModule.forRoot(), //if this is your app.module
    NbButtonModule,    
    NbCardModule
  ],
```
## Problemas encontrados
1. Error icons: No se muestran los íconos a pesar de haber importado el módulo
- Agregar a las importaciones de módulos: NbEvaIconsModule
- https://stackoverflow.com/questions/65472862/how-to-fix-the-error-error-default-pack-is-not-registered-in-nebular
- https://akveo.github.io/nebular/docs/guides/register-icon-pack#register-icon-pack
- https://akveo.github.io/eva-icons/#/
