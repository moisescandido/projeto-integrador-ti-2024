from utime import sleep
import machine
import urequests
import network
import dht
 
sensor = dht.DHT22(machine.Pin(16))
led_vermelho = machine.Pin(20, machine.Pin.OUT)
led_verde = machine.Pin(19, machine.Pin.OUT)
wlan = network.WLAN(network.STA_IF)

def erro():
    led_vermelho.value(True)
    led_verde.value(False)

 
def funcionando():
    led_vermelho.value(False)
    led_verde.value(True)
 

def connect():
    ssid = 'admin'
    password = 'admin@123'

    wlan.active(False)
    wlan.active(True)
    wlan.connect(ssid, password)
 
    try:
        while not wlan.isconnected():
            print('Aguardando conexão')
            erro()
            sleep(2)
        print("Conectado!")
    except Exception as e:
        print("Erro ao conectar:", e)
        erro()
    sleep(2)
 
def enviar_dados(umidade, temperatura):
    try:
        url = "http://climasenac.somee.com/clima/{}/{}".format(temperatura, umidade)
        token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlJhc3BiZXJyeSBQaWNvIFciLCJuYmYiOjE3MTAwOTAyNTgsImV4cCI6MTc3MzE2MjI1OCwiaWF0IjoxNzEwMDkwMjU4fQ.yFKew7HedDnwNjfKCp3_JGWxReBN_v3vCyfEjfcjyLY"
        headers = {"Content-Type": "application/json",
                   "Authorization": token}
        response = urequests.get(url, headers=headers)

                   
        if(response.status_code == 403):
            print("Servidor offline")
            erro()
        
        if(response.status_code == 200 or response.status_code == 405):
            funcionando()
            print("Dados enviados com sucesso")
        else:
            print("Erro ao enviar dados!")
            print(response.status_code)
            print(response.text)
        response.close()
    except Exception as e:
        print("Erro ao enviar requisição:", e)
        erro()

 
def temperatura_umidade():
    try:
        sensor.measure()
        print("Umidade: {} : Temperatura: {}".format(sensor.humidity(), sensor.temperature()))
        enviar_dados(sensor.humidity(), sensor.temperature())
    except Exception as e:
        print("Não foi possivel obter dados do sensor DHT22!")
        return
        
def main():
    while True:
        if wlan.isconnected():
            funcionando()
            try:
                temperatura_umidade()
            except Exception as e:
                print("Erro:", e)
                erro()
            finally:
                sleep(3)
        else:
            print("Rede inativa!\n Conectando!")
            connect()
            main()
main()