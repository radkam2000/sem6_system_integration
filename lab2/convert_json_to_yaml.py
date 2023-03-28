# -*- coding: utf-8 -*-
"""
json to yaml converter
"""
import yaml
import json
class ConvertJsonToYaml:
    @staticmethod
    def run(deserializeddata, destinationfilelocaiton):
        print("let's convert something")
        with open(destinationfilelocaiton, 'w', encoding='utf-8') as f: yaml.dump(deserializeddata, f, allow_unicode=True)
        print("it is done")
    @staticmethod
    def run_location(jsonLocation, yamlLocation):
        print("let's convert something")
        with open(jsonLocation, 'r', encoding='utf8') as f:
            data = json.load(f)
        with open(yamlLocation, 'w', encoding='utf8') as f:
            yaml.dump(data, f, allow_unicode=True)
        print("it is done")