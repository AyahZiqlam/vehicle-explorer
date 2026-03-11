# vehicle-explorer
A web application that allows users to search for vehicle makes and retrieve available vehicle types and models based on a selected year.

## Technologies

- Angular
- RxJS
- Angular Material
- Docker

## Features

- Autocomplete search for vehicle makes
- Retrieve vehicle types and models from NHTSA API
- Parallel API requests using RxJS forkJoin
- Responsive UI

## Run Locally

1. Clone the repo

git clone https://github.com/AyahZiqlam/vehicle-explorer.git

2. Install dependencies

npm install

3. Start the application

ng serve

Open:

http://localhost:4200


## Run with Docker

Build the image

docker build -t vehicle-explorer .

Run container

docker run -p 8080:80 vehicle-explorer

Open:

http://localhost:8080
