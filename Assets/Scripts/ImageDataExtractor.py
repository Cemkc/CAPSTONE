import sys

import pytesseract
import cv2
import json

def rgb_to_hex(rgb):
    return '0x' + '%02x%02x%02x' % rgb


image_path = sys.argv[1]
output_dir = sys.argv[2]

img = cv2.imread(image_path)
gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)  # Convert image to grayscale
_, thresh = cv2.threshold(gray, 210, 255, cv2.THRESH_BINARY_INV)  # Set a high threshold as 210 to detect bright-colored
# rectangles
contours, _ = cv2.findContours(thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)  # Use RETR_EXTERNAL to exclude
# numbers inside the rectangle by getting only parent contours

buildings_dict = {}

for i, contour in enumerate(contours):
    x, y, w, h = cv2.boundingRect(contour)
    color_data = img[y + 10, x + 10]  # add 10 to get away from the edges and get the certain color data.
    color_data_rgb = (color_data[2], color_data[1], color_data[0])
    color_hex = rgb_to_hex(color_data_rgb)  # Color data in hexadecimal
    print("Starting point: ({}, {}), Width: {}, Height: {},".format(x, y, w, h), "Color:", color_hex)
    roi = gray[y:y + h, x:x + w]
    number_text = pytesseract.image_to_string(roi, config='--psm 6').strip()
    print("Height of the building: " + number_text)

    dict_key = "Building" + str(i)
    buildings_dict[dict_key] = {'PosX': x, 'PosY': y, 'LengthX': w, 'LengthY': h,
                                'Color': color_hex, 'Floors': int(number_text)}

buildings_json = json.dumps(buildings_dict, indent=4)
print(buildings_json)

with open(output_dir + "buildings_data.json", "w") as outfile:
    outfile.write(buildings_json)



