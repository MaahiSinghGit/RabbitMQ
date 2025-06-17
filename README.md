# 🐇 RabbitMQ with .NET — Producer & Consumer Demo

A simple yet powerful **message queueing** demo using **RabbitMQ** with **.NET Console Applications**. This example shows how a `Producer` sends messages and a `Consumer` processes them using a **Direct Exchange**.

---

## 📦 Project Overview

RabbitMQ-Demo/

├── Producer/ # Sends messages to RabbitMQ

├── Consumer/ # Listens and processes messages

└── README.md # You're here!



This setup demonstrates a **one-to-one** messaging pattern using `RabbitMQ.Client` with support for:
- Persistent messages
- Manual acknowledgments
- Asynchronous message consumption

---

## 🐳 1. Start RabbitMQ with Docker

Spin up RabbitMQ with the management UI:

```bash
docker run -d --hostname rmq --name rabbit-server \
-p 15672:15672 -p 5672:5672 rabbitmq:3-management

```
🔗 RabbitMQ Access
Web UI: http://localhost:15672

Username: guest

Password: guest


⚙️ 2. Setup Producer & Consumer Apps

🧱 Create Projects
```bash
# Producer
dotnet new console -n Producer
cd Producer
dotnet add package RabbitMQ.Client --version 7.1.2

# Consumer
dotnet new console -n Consumer
cd Consumer
dotnet add package RabbitMQ.Client --version 7.1.2

```
##  🚀 3. How It Works

🔁 Message Flow Diagram

```css
                   ┌──────────────────┐
                   │   Producer       │
                   └────────┬─────────┘
                            │
                     Publishes Messages
                            │
                   ┌────────▼─────────┐
                   │    Exchange      │
                   │ (Direct Routing) │
                   └────────┬─────────┘
                            │
                   ┌────────▼─────────┐
                   │      Queue       │
                   └────────┬─────────┘
                            │
                   ┌─────────▼────────┐
                   │     Consumer     │
                   └──────────────────┘
```
##  🧠 Flow Description

Producer sends a message with a specific routing key

Exchange (direct type) routes the message to a queue bound with that key

Queue stores the message until the Consumer reads and acknowledges it

##  📂 Code Overview

You’ll find complete code inside the Producer/ and Consumer/ folders.

##  ✅ Producer Highlights

Connects to RabbitMQ

Declares an exchange and queue

Publishes 10 messages (1 every 5 seconds)

Uses a routing key and persistent messages

##  ✅ Consumer Highlights

Connects to RabbitMQ

Binds to the same exchange/queue

Listens for messages using AsyncEventingBasicConsumer

Uses manual ack to confirm processing

##  Producer Output:

```
Sent: Message 1: Hello, RabbitMQ!

```

##  Consumer Output:

```
Received: Message 1: Hello, RabbitMQ!

```

##  Made with ❤️ using RabbitMQ and .NET

## Feel free to fork, contribute, or reach out with suggestions!
