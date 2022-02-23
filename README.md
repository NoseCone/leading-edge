# Nose Cone in Feliz

Before developing or building for deployment run:
```
> dotnet tool restore
> dotnet paket install
```

## Developing
Work on the stylesheet `site.sass` with:
```bash
> cd src
> sass --watch site.sass:site.css --load-path ../node_modules
```
Install dependencies with `npm install` (or `pnpm install` or `yarn`) and start a development server with:
```bash
npm run dev
# or start the server and open the app in a new browser tab
npm run dev -- --open
```
## Building
Build the stylesheet `site.css` with:
```bash
> cd src
> sass site.sass:site.css --load-path ../node_modules
```

Build for deployment with:
```bash
npm run build
```
> You can preview the built app with `npm run preview`. This should _not_ be used to serve your app in production.
## Publishing
Build on branch `develop`, switch to branch `main` and then copy the build to the root, commit and push.
```bash
> cp -a build/ .
```
