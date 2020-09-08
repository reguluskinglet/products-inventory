FROM node:10.16.3-alpine as build-deps
WORKDIR /app
COPY ./client-app/package.json .
COPY ./client-app/yarn.lock .
RUN yarn
COPY ./client-app .
RUN yarn build

FROM nginx:1.17.2-alpine
COPY --from=build-deps /app/build /usr/share/nginx/html
COPY ./client-app/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
