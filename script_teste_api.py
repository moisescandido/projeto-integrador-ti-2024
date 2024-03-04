import requests
import random
import time

# Função para gerar dados aleatórios de temperatura e umidade
def generate_data():
    temperatura = random.uniform(0, 100)
    umidade = random.uniform(0, 100)
    return temperatura, umidade

# URL do servidor
# Loop infinito para enviar solicitações POST
while True:
    # Gerar dados aleatórios
    temperatura, umidade = generate_data()
    url = f'http://localhost:5091/clima?temperatura={temperatura}&umidade={umidade}'
    # Corpo da requisição

    # Enviar solicitação POST
    try:
        response = requests.post(url)
        if response.status_code == 200:
            print("Dados enviados com sucesso")
            print(f"Umidade: {umidade} : Temperatura: {temperatura}")
        else:
            print("Erro ao enviar dados:", response.status_code)
    except Exception as e:
        print("Erro ao enviar requisição:", e)


    # Esperar um intervalo de tempo antes de enviar a próxima solicitação
    time.sleep(5)  # Intervalo de 5 segundos

