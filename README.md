## Build

Go to the root folder of the Project directory and run the following code snippet

`
dotnet build
`

## Run

Go to the root folder of the Project directory and run the following code snippet

`
dotnet run
`

## Swagger API Documentation

Navigate to http://localhost:{port}/swagger for the Mars Rover Api documentation and to test with Swagger

### /MarsRover/DownloadAndSaveApods

**Reads** from a `data.txt` file in the Project's `Data` directory

**Writes** to the Project's `Images` directory with the filename and type from the Url
