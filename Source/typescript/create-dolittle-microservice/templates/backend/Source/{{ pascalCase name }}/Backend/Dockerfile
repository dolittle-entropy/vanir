FROM node:14.2.0 AS shared-build

WORKDIR /app

COPY package.json ./
COPY .eslintrc.js ./
COPY tsconfig.json ./
COPY application.json ./

COPY Source/Shared/Backend/package.json ./Source/Shared/Backend/package.json
COPY Source/Shared/Common/package.json ./Source/Shared/Common/package.json
COPY Source/Shared/Specs/package.json ./Source/Shared/Specs/package.json
COPY Source/Shared/Styles/package.json ./Source/Shared/Styles/package.json
COPY Source/Shared/Web/package.json ./Source/Shared/Web/package.json
RUN yarn

COPY Source/{{name}}/microservice.json /app/Source/{{name}}/microservice.json

# Backend Build
FROM node:14.2.0 AS backend-build

WORKDIR /app

COPY --from=shared-build /app/ ./

WORKDIR /app/Source/{{name}}/Backend
COPY Source/{{name}}/Backend/package.json ./package.json
RUN yarn

COPY Source/Shared /app/Source/Shared/
COPY Source/{{name}}/Backend/ ./

RUN yarn build

# Resulting Image
FROM node:14.2.0-slim as base
ARG CONFIGURATION=Release

RUN echo Configuration = $CONFIGURATION

WORKDIR /app
COPY --from=backend-build /app/Source/{{name}}/Backend/dist/index.js ./
COPY --from=backend-build /app/Source/{{name}}/Backend/config.json ./
COPY --from=backend-build /app/Source/{{name}}/Backend/public/index.html ./public/
COPY --from=backend-build /app/node_modules/swagger-ui-dist/swagger-ui.css ./public/api/{{name}}/swagger/
COPY --from=backend-build /app/node_modules/swagger-ui-dist/swagger-ui-standalone-preset.js ./public/api/{{name}}/swagger/
COPY --from=backend-build /app/node_modules/swagger-ui-dist/swagger-ui-bundle.js ./public/api/{{name}}/swagger/
COPY --from=backend-build /app/node_modules/swagger-ui-dist/favicon-16x16.png ./public/api/{{name}}/swagger/
COPY --from=backend-build /app/node_modules/swagger-ui-dist/favicon-32x32.png ./public/api/{{name}}/swagger/
COPY --from=backend-build /app/Source/{{name}}/Backend/swagger.json ./

ENTRYPOINT ["node", "index.js"]
