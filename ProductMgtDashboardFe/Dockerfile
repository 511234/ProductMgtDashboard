FROM node:20.19.2-alpine

WORKDIR /app

COPY ProductMgtDashboardFe/package.json .

RUN npm install

COPY . .

EXPOSE 5173

CMD [ "npm", "run", "dev" ]