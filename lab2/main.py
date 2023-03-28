print("hey, it's me - Python!")

import yaml
from deserialize_json import DeserializeJson
from convert_json_to_yaml import ConvertJsonToYaml
from serialize_json import SerializeJson

tempconffile = open('Assets/basic_config.yaml',encoding="utf8")
confdata = yaml.load(tempconffile, Loader=yaml.FullLoader)
print(confdata)

operationRange = confdata['opertaion_range']
serializeType = confdata['serialize_type']
operationQueue = confdata['opertaion_queue']

for i in operationQueue:
    if i == "deserialize_json":
        newDeserializator = DeserializeJson(confdata['paths']['source_folder'] + confdata['paths']['json_source_file'])
    if (serializeType == "object"):

    elif(serializeType == "file"):
        pass
    else:
        print("config error")

newDeserializator = DeserializeJson(confdata['paths']['source_folder']+confdata['paths']['json_source_file'])
newDeserializator.somestats()

ConvertJsonToYaml.run(newDeserializator,confdata['paths']['source_folder']+confdata['paths']['yaml_destination_file'])
ConvertJsonToYaml.run_location(confdata['paths']['source_folder']+confdata['paths']['json_source_file'],confdata['paths']['source_folder']+confdata['paths']['yaml_destination_file'])