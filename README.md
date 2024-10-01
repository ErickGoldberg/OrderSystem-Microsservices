# OrderSystem - Microsservices

Este projeto é um sistema de pedidos baseado em microsserviços, utilizando RabbitMQ, API Gateway com Ocelot e arquitetura limpa.

## Tecnologias Utilizadas

- **RabbitMQ** para mensageria
- **Ocelot** como API Gateway
- **EF Core** para ORM
- **MediatR** para logging e mensagens
- **JWT** para autenticação
- **Unit of Work** e **Repository Pattern**
- **Cache** e **Middlewares**

## Estrutura dos Serviços

- **OrderSystem.ApiGateway**: Gestão de rotas de comunicação entre microsserviços.
- **OrderSystem.DeliveryService**: Lida com a lógica de entregas.
- **OrderSystem.OrderService**: Gerencia a lógica de pedidos.
- **OrderSystem.PaymentService**: Processa pagamentos.
- **OrderSystem.Common**: Contém classes compartilhadas entre os serviços.

## Como rodar o projeto

1. Clone o repositório.
2. Configure o RabbitMQ, o Gateway e os serviços.
3. Compile e execute cada serviço separadamente.
