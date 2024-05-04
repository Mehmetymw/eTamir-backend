package main

import "github.com/spf13/viper"

type Server struct {
	Host string
	Port int
}

type Config struct {
	Server         Server
	Db             string
	CollectionName string
}

func NewConfig() *Config {

	viper.SetConfigFile("config.yaml")

	err := viper.ReadInConfig()
	if err != nil {
		panic(err)
	}

	return &Config{
		Server: Server{
			Host: viper.GetString("server.host"),
			Port: viper.GetInt("server.port"),
		},
		Db:             viper.GetString("db"),
		CollectionName: viper.GetString("collectionName"),
	}
}
