# Financial Transaction Tracker

## Overview
This project is a basic financial transaction tracking system built with a React frontend and a .NET Core backend. Users can add transactions, view their history, filter transactions, and see their total balance.

## Tech Stack
- **Frontend:** React with TypeScript
- **Backend:** .NET Core Web API, Entity Framework Core
- **Database:** SQL Server
- **Deployment:** AWS Elastic Beanstalk using GitHub Actions

## Approach
1. **Backend:**  
   - Designed a simple API using .NET Core.
   - Implemented endpoints to add transactions, retrieve filtered transaction lists, and calculate the balance.
   - Used EF Core for data persistence, with a code-first approach.
   
2. **Frontend:**  
   - Built a React application using functional components and hooks.
   - Created components for transaction listing, filtering, and adding new transactions.
   - Integrated with the backend using Fetch for API calls.

## Challenges
- Initially, I tried to deploy the frontend to Vercel. However, Vercel provides an HTTPS URL while my API was hosted on HTTP in Elastic Beanstalk, which caused issues with API requests due to mixed content restrictions. The alternatives were to purchase a domain and set up a load balancer with proper credentials, but I determined it would be less work to deploy the frontend to Elastic Beanstalk as well, which is the approach I ultimately took.
- Designing flexible filtering logic on the backend to handle multiple query parameters.
- Ensuring seamless communication between the React frontend and the .NET API.
- Managing state in React effectively while handling asynchronous API calls.
- Configuring a CI/CD pipeline with GitHub Actions for automated deployment to AWS Elastic Beanstalk, including secure management of AWS credentials, build automation, and handling environment updates.

## Learnings
- Learned best practices for structuring a full-stack application with a clear separation of concerns.
- Enhanced my skills in setting up and troubleshooting CI/CD pipelines, particularly with AWS Elastic Beanstalk and GitHub Actions.
- Learned how to improve security and effectively manage secrets in the deployment pipeline.
- Improved my Docker knowledge for both .NET and React.js, which helped in containerizing the application for consistent development and deployment environments.
